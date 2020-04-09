using System;
using Microsoft.Azure.ServiceBus;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusSendMessageQueue
    {
        private readonly IQueueClient _queueClient;
        public ServiceBusSendMessageQueue(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task SendMessagesAsync(object message)
        {
            try
            {
                var msg = new Message(Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(message)));

                await _queueClient.SendAsync(msg);

                await _queueClient.CloseAsync();
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
