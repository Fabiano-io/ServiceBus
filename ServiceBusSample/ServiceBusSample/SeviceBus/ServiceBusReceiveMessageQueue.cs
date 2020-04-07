using Microsoft.Azure.ServiceBus;
using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusSample.SeviceBus
{
    class ServiceBusReceiveMessageQueue
    {
        private string _serviceBusConnectionString;
        private string _queueName;

        private static IQueueClient _queueClient;

        public ServiceBusReceiveMessageQueue()
        {
            _serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"];
            _queueName = ConfigurationManager.AppSettings["QueueName"];
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var sequencia = message.SystemProperties.SequenceNumber;
            var mensagem = Encoding.UTF8.GetString(message.Body);

            Console.WriteLine($"Sequencia: {sequencia} - Mensagem: {mensagem}.");

            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is opened in ReceiveMode.PeekLock mode (which is default).
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        // Use this Handler to look at the exceptions received on the MessagePump
        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var erro = exceptionReceivedEventArgs.Exception;

            Console.WriteLine($"Message handler encountered an exception {erro}.");
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
                // False value below indicates the Complete will be handled by the User Callback as seen in `ProcessMessagesAsync`.
                AutoComplete = false
            };

            // Register the function that will process messages
            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        public void IniciaVerificacaoFila()
        {
            _queueClient =  new QueueClient(_serviceBusConnectionString, _queueName);

            // Register QueueClient's MessageHandler and receive messages in a loop
            RegisterOnMessageHandlerAndReceiveMessages();
        }

        public async Task PararVerificacaoFila()
        {
            await _queueClient.CloseAsync();
        }
    }
}
