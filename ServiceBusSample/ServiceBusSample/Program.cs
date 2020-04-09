using ServiceBusSample.SeviceBus;
using System;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new ServiceBusSendMessage();

            // Enviando mensagem para a Fila
            var produtos = new
            {
                Produto = "Nome do Produto",
                Quantidade = 10
            };
            sb.SendMessage("fila-teste", produtos).GetAwaiter().GetResult();



            // Enviando mensagem para o tópico 
            var dados = new
            {
                Campo1 = "Valor do Campo 1",
                Campo2 = "Valor do Campo 2"
            };
            sb.SendMessage("topico-teste", dados, new CorrelationFilter()).GetAwaiter().GetResult();


            CorrelationFilter filtro = new CorrelationFilter
            {
                Label = "cliente"   // Os filtros são case sensitive
            };
            var cliente = new
            {
                Nome = "Nome do Cliente"
            };
            sb.SendMessage("topico-teste", cliente, filtro).GetAwaiter().GetResult();


            filtro.Label = "endereco";
            var endereco = new
            {
                Rua = "Nome da rua",
                Numero = 10,
                Bairro = "Nome do Bairro",
                Cidade = "Nome da Cidade"
            };
            sb.SendMessage("topico-teste", endereco, filtro).GetAwaiter().GetResult();







            // Recebendo as mensagens da fila
            var sbrmq = new ServiceBusReceiveMessageQueue();
            sbrmq.IniciaVerificacaoFila("fila-teste");
            Console.WriteLine("Verificando Fila.\n\rPressione qualquer tecla para finalizar a aplicação.");
            Console.ReadKey();
            sbrmq.PararVerificacaoFila().GetAwaiter().GetResult();



            //Recebendo mensagens do topico
            var sbrmt = new ServiceBusReceiveMessageTopic();
            sbrmt.IniciaVerificacaoFila("topico-teste", "assinatura-endereco");
            Console.WriteLine("Verificando Fila.\n\rPressione qualquer tecla para finalizar a aplicação.");
            Console.ReadKey();
            sbrmt.PararVerificacaoFila().GetAwaiter().GetResult();

        }
    }
}
