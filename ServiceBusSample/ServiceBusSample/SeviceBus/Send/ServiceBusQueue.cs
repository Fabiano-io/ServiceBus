using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusSample.SeviceBus
{
    
    class ServiceBusQueue
    {
        private string _serviceBusConnectionStringSend;
        public ServiceBusQueue()
        {
            _serviceBusConnectionStringSend = ConfigurationManager.AppSettings["ServiceBusConnectionStringSend"];
        }

        public QueueClient GetQueue(string QueueName)
        {
            return new QueueClient(_serviceBusConnectionStringSend, QueueName);
        }

    }
}
