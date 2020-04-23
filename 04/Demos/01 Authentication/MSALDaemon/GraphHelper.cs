using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace MSALDaemon
{
    public class GraphHelper
    {

        private static void sendMail(GraphCfg gconfig, Message msg, string SenderAcct, AILogger logger)
        {
            try
            {
                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(gconfig.clientId)
                    .WithTenantId(gconfig.tenantId)
                    .WithClientSecret(gconfig.clientSecret)
                    .Build();

                ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

                GraphServiceClient graphClient = new GraphServiceClient(authProvider);
                graphClient.Users[SenderAcct].SendMail(msg, false).Request().PostAsync();
                List<QueryOption> options = new List<QueryOption> {
                    new QueryOption ("$top", "1")
                };

                var graphResult = graphClient.Users.Request(options).GetAsync().Result;
                var props = new Dictionary<string, string> { { "mail", msg.Body.ToString() } };
                logger.LogEvent("mail-send", props);
            }
            catch (System.Exception ex)
            {
                logger.LogEvent("mail-send-error", ex);
            }
        }

        private static void AddReciepient(List<Recipient> toRecipientsList, string r)
        {
            var emailAddress = new Microsoft.Graph.EmailAddress
            {
                Address = r,
            };

            var toRecipients = new Recipient
            {
                EmailAddress = emailAddress,
            };
            toRecipientsList.Add(toRecipients);
        }

        public static bool Send(string Subject, string Message, string[] Recipient, GraphCfg config, AILogger logger)
        {
            var result = false;

            var recipients = new List<Recipient>();

            foreach (var r in Recipient)
            {
                AddReciepient(recipients, r);
            }


            var body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = Message,
            };

            Message message = new Message
            {
                Subject = Subject,
                Body = body,
                ToRecipients = recipients,
            };

            config.returnUrl = config.frontendUrl;
            sendMail(config, message, config.mailSender, logger);

            result = true;
            return result;
        }
    }
}