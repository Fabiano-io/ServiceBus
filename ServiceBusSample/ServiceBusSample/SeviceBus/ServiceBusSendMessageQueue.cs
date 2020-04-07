using Microsoft.Azure.ServiceBus;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusSendMessageQueue
    {
        private string _serviceBusConnectionString;
        private string _queueName; 
        private static IQueueClient _queueClient;

        public ServiceBusSendMessageQueue()
        {
            _serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"];
            _queueName = ConfigurationManager.AppSettings["QueueName"];
        }
        public async Task SendMessagesAsync(string message)
        {
            _queueClient = new QueueClient(_serviceBusConnectionString, _queueName);

            var msg = new Message(Encoding.UTF8.GetBytes(message));

            await _queueClient.SendAsync(msg);

            await _queueClient.CloseAsync();
        }
    }
}
