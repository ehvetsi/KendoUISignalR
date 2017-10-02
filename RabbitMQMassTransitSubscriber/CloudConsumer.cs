using MassTransit;
using System;
using System.Threading.Tasks;
using Unifique.ServiceBroker.Domain.Interfaces.ServicesProxy;

namespace RabbitMQMassTransitSubscriber
{
    public class CloudConsumer : IConsumer<IBrokerProxy>
    {
        public Task Consume(ConsumeContext<IBrokerProxy> context)
        {
            Console.WriteLine($"Mensagem recebida: {context.Message}");

            return Task.FromResult(0);
        }
    }
}