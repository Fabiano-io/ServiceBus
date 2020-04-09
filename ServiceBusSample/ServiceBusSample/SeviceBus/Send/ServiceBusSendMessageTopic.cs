using System;
using System.Collections.Generic;
using Microsoft.Azure.ServiceBus;
using System.Configuration;
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
                var msg = new Message(Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(message)));

                msg.ContentType = filter.ContentType;
                msg.CorrelationId = filter.CorrelationId;
                msg.Label = filter.Label;
                msg.ReplyTo = filter.ReplyTo;
                msg.ReplyToSessionId = filter.ReplyToSessionId;
                msg.SessionId = filter.SessionId;
                msg.To = filter.To;

                await _topicClient.SendAsync(msg);

                await _topicClient.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
