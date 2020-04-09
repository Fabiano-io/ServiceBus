using System.Configuration;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusTopic
    {
        private readonly string _serviceBusConnectionStringSend;

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
