using System.Configuration;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusSample.SeviceBus
{
    
    class ServiceBusQueue
    {
        private readonly string _serviceBusConnectionStringSend;
        public ServiceBusQueue()
        {
            _serviceBusConnectionStringSend = ConfigurationManager.AppSettings["ServiceBusConnectionStringSend"];
        }

        public QueueClient GetQueue(string queueName)
        {
            return new QueueClient(_serviceBusConnectionStringSend, queueName);
        }

    }
}
