using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusTopic
    {

        private string _serviceBusConnectionStringSend;

        public ServiceBusTopic()
        {
            _serviceBusConnectionStringSend = ConfigurationManager.AppSettings["ServiceBusConnectionStringSend"];
        }

        public ITopicClient GetTopic(string topicName)
        {
            return new TopicClient(_serviceBusConnectionStringSend, topicName);
        }
    }
}
