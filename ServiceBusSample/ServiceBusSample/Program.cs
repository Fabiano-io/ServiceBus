using ServiceBusSample.SeviceBus;
using System;

namespace ServiceBusSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enviano mensagem para a Fila
            var sbsmq = new ServiceBusSendMessageQueue();
            sbsmq.SendMessagesAsync("Mensagem Teste 01").GetAwaiter().GetResult();
            sbsmq.SendMessagesAsync("Mensagem Teste 02").GetAwaiter().GetResult();
            sbsmq.SendMessagesAsync("Mensagem Teste 03").GetAwaiter().GetResult();


            // Recebendo as mensagens da fila
            var sbrmq = new ServiceBusReceiveMessageQueue();
            sbrmq.IniciaVerificacaoFila();

            Console.WriteLine("Verificando Fila.\n\rPressione qualquer tecla para finalizar a aplicação.");
            Console.ReadKey();
            sbrmq.PararVerificacaoFila().GetAwaiter().GetResult();


            // Enviando mensagens para o topico
            var sbsmt = new ServiceBusSendMessageTopic();
            sbsmt.SendMessagesAsync("Mensagem Teste 01").GetAwaiter().GetResult();
            sbsmt.SendMessagesAsync("Mensagem Teste 02").GetAwaiter().GetResult();
            sbsmt.SendMessagesAsync("Mensagem Teste 03").GetAwaiter().GetResult();


            // Recebendo mensagens do topico
            var sbrmt = new ServiceBusReceiveMessageTopic();
            sbrmt.IniciaVerificacaoFila();

            Console.WriteLine("Verificando Fila.\n\rPressione qualquer tecla para finalizar a aplicação.");
            Console.ReadKey();
            sbrmt.PararVerificacaoFila().GetAwaiter().GetResult();

        }
    }
}
