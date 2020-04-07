using Microsoft.Azure.ServiceBus;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusSendMessageTopic
    {
        private string _serviceBusConnectionString;
        private string _topicName;
        private string _subscriptionName;

        private static ITopicClient _topicClient;
        private static ISubscriptionClient _subscriptionClient;

        public ServiceBusSendMessageTopic()
        {
            _serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"];
            _topicName = ConfigurationManager.AppSettings["TopicName"];
            _subscriptionName = ConfigurationManager.AppSettings["SubscriptionName"];
        }

        public async Task SendMessagesAsync(string message)
        {
            _topicClient = new TopicClient(_serviceBusConnectionString, _topicName);
            _subscriptionClient = new SubscriptionClient(_serviceBusConnectionString, _topicName, _subscriptionName);

            var msg = new Message(Encoding.UTF8.GetBytes(message));

            await _topicClient.SendAsync(msg);

            await _subscriptionClient.CloseAsync();
            await _topicClient.CloseAsync();
        }
    }
}
