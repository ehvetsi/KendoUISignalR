using CrossCutting.Interfaces;
using KendoUISignalR.Services.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KendoUISignalR.Services
{
    public class ProductService
    {
        public async Task ProductQueuer(ProductMessage message)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
              x.Host(new Uri("rabbitmq://localhost/"), h =>
              {
              }));
            bus.Start();
            var sender = await bus.GetSendEndpoint(new Uri("rabbitmq://localhost/product_queue"));
            await sender.Send<IProductMessage>(message);
            //bus.Publish<IProductMessage>(message);
        }
    }
}