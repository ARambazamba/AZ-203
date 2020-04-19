using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueueApp {
    class Program {

        const string ConnectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=uqueuelab;AccountKey=sBXu5KV/vCeOA7XcimH/NRo0oUAGOQVaYdb73FZGJ+0fqPpb1Y5E7mhxM3gKVU7q2RxB4VRgNsyLHoyEBaMkiA==";

        static async Task Main (string[] args) {
            if (args.Length > 0) {
                string value = String.Join (" ", args);
                SendArticleAsync (value);
                Console.WriteLine ($"Sent: {value}");
            } else {
                string value = await ReceiveArticleAsync ();
                Console.WriteLine ($"Received {value}");
            }
        }

        static async Task SendArticleAsync (string newsMessage) {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse (ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient ();

            CloudQueue queue = queueClient.GetQueueReference ("newsqueue");
            bool createdQueue = await queue.CreateIfNotExistsAsync ();
            if (createdQueue) {
                Console.WriteLine ("The queue of news articles was created.");
            }

            CloudQueueMessage articleMessage = new CloudQueueMessage (newsMessage);
            await queue.AddMessageAsync (articleMessage);
        }

        static CloudQueue GetQueue () {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse (ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient ();
            return queueClient.GetQueueReference ("newsqueue");
        }

        static async Task<string> ReceiveArticleAsync () {
            CloudQueue queue = GetQueue ();
            bool exists = await queue.ExistsAsync ();
            if (exists) {
                CloudQueueMessage retrievedArticle = await queue.GetMessageAsync ();
                if (retrievedArticle != null) {
                    string newsMessage = retrievedArticle.AsString;
                    await queue.DeleteMessageAsync (retrievedArticle);
                    return newsMessage;
                }
            }

            return "<queue empty or not created>";
        }
    }
}