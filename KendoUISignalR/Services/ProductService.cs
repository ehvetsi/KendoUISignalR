using CrossCutting.Interfaces;
using KendoUISignalR.Services.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoUISignalR.Services
{
    public class ProductService
    {
        public void ProductQueuer(ProductMessage message)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
              x.Host(new Uri("rabbitmq://localhost/"), h =>
              {
              }));
            bus.Start();

            bus.Publish<IProductMessage>(message);
        }
    }
}