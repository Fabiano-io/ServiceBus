using Microsoft.Azure.ServiceBus;
using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusReceiveMessageTopic
    {
        private string _serviceBusConnectionString;
        private string _topicName;
        private string _subscriptionName;

        private static ITopicClient _topicClient;
        private static ISubscriptionClient _subscriptionClient;

        public ServiceBusReceiveMessageTopic()
        {
            _serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"];
            _topicName = ConfigurationManager.AppSettings["TopicName"];
            _subscriptionName = ConfigurationManager.AppSettings["SubscriptionName"];
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // Process the message
            var sequencia = message.SystemProperties.SequenceNumber;
            var mensagem = Encoding.UTF8.GetString(message.Body);

            Console.WriteLine($"Sequencia: {sequencia} - Mensagem: {mensagem}.");

            // Complete the message so that it is not received again.
            // This can be done only if the subscriptionClient is opened in ReceiveMode.PeekLock mode (which is default).
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        // Use this Handler to look at the exceptions received on the MessagePump
        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            // Register the function that will process messages
            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        public void IniciaVerificacaoFila()
        {
            _topicClient = new TopicClient(_serviceBusConnectionString, _topicName);
            _subscriptionClient = new SubscriptionClient(_serviceBusConnectionString, _topicName, _subscriptionName);

            // Register QueueClient's MessageHandler and receive messages in a loop
            RegisterOnMessageHandlerAndReceiveMessages();
        }

        public async Task PararVerificacaoFila()
        {
            await _subscriptionClient.CloseAsync();
            await _topicClient.CloseAsync();
        }
    }
}
