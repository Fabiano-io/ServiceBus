using System;
using Microsoft.Azure.ServiceBus;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusSendMessageTopic
    {
        private readonly ITopicClient _topicClient;
        public ServiceBusSendMessageTopic(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }

        public async Task SendMessagesAsync(object message, CorrelationFilter filter)
        {
            try
            {
                var msg = new Message(Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(message)))
                {
                    ContentType = filter.ContentType,
                    CorrelationId = filter.CorrelationId,
                    Label = filter.Label,
                    ReplyTo = filter.ReplyTo,
                    ReplyToSessionId = filter.ReplyToSessionId,
                    SessionId = filter.SessionId,
                    To = filter.To
                };

                await _topicClient.SendAsync(msg);

                await _topicClient.CloseAsync();
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"Erro: {ex.Message}");
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
