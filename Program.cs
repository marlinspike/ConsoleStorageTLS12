using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Threading.Tasks;

namespace BlobQuickstartV12 {
    class Program {
        static async Task Main(string[] args) {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var cn = config.GetValue<String>("StorageSAS");
            var containerName = config.GetValue<String>("ContainerName");

            BlobServiceClient blobClient = new BlobServiceClient(cn);
            BlobContainerClient containerClient = blobClient.GetBlobContainerClient(containerName);


            Console.WriteLine("Listing blobs...");

            // List all blobs in the container
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync()) {
                Console.WriteLine("\t" + blobItem.Name);
            }
        }
    }
}