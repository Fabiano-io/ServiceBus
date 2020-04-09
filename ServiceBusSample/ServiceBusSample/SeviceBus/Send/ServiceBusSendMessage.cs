using System;
using System.Diagnostics;
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
            try
            {
                var sbsm = new ServiceBusSendMessageTopic(new ServiceBusTopic().GetTopic(topicName));

                await sbsm.SendMessagesAsync(message, filter);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"Erro: {ex.Message}");
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task SendMessage(string queueName, object message)
        {
            try
            {
                var sbsm = new ServiceBusSendMessageQueue(new ServiceBusQueue().GetQueue(queueName));

                await sbsm.SendMessagesAsync(message);
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
