using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQMassTransitSubscriber
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h => { });

                x.ReceiveEndpoint(host, "rpc_queue", e =>
            e.Consumer<ProductConsumer>());
            });
            bus.Start();
            Console.ReadKey();
            bus.Stop();
        }
    }
}