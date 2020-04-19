using System;
using System.Configuration;
using System.IO;
using CosmosTableSamples;
using CosmosTableSamples.Model;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace TableStorage
{
    class Program
    {
        static void Main(string[] args)
        {

            // Access app settings
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var conStr = configuration["ConnectionStrings"];

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conStr);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customers");

            table.CreateIfNotExists();
            Console.ReadKey();

            Samples.Run(table).Wait();
        }
    }
}