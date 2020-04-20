using System;
using System.IO;
using Azure.Storage.Blobs;

namespace WebJob {
    class Program {
        static void Main (string[] args) {
            string connectionString = Environment.GetEnvironmentVariable ("AZURE_STORAGE_CONNECTION_STRING");
            Console.WriteLine ("Got Connection String:" + connectionString);
            BlobServiceClient blobServiceClient = new BlobServiceClient (connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient ("webjob");
            string localPath = "./data/";
            string fileName = "demo" + Guid.NewGuid ().ToString () + ".txt";
            string localFilePath = Path.Combine (localPath, fileName);
            File.WriteAllText (localFilePath, "Hello World");
            var blobClient = containerClient.GetBlobClient ("demofile.txt");
            using FileStream uploadFileStream = File.OpenRead (localFilePath);
            blobClient.Upload (uploadFileStream, true);
            uploadFileStream.Close ();
        }
    }
}