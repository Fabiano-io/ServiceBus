using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusSample.SeviceBus
{
    public class ServiceBusSendMessage
    {

        public ServiceBusSendMessage()
        {
            
        }

        public async Task SendMessage(string topicName, object message, CorrelationFilter filter)
        {
            ServiceBusSendMessageTopic sbsm = new ServiceBusSendMessageTopic(new ServiceBusTopic().GetTopic(topicName));

            await sbsm.SendMessagesAsync(message, filter);

        }

        public async Task SendMessage(string queueName, object message)
        {
            ServiceBusSendMessageQueue sbsm = new ServiceBusSendMessageQueue(new ServiceBusQueue().GetQueue(queueName));

            await sbsm.SendMessagesAsync(message);

        }
    }
}
