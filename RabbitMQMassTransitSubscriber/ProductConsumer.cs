using CrossCutting;
using CrossCutting.Interfaces;
using KendoUISignalR.CrossCutting.Dto;
using KendoUISignalR.Hubs;
using KendoUISignalR.Models;
using MassTransit;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQMassTransitSubscriber
{
    public class ProductConsumer : IConsumer<IProductMessage>
    {
        public Task Consume(ConsumeContext<IProductMessage> context)
        {
            using (var dbContext = new SampleEntities())
            {
                var product = dbContext.Products.FirstOrDefault(c => c.ProductID == context.Message.Product.ProductID);
                product.ProductName = product.ProductName + DateTime.Now.Millisecond;
                dbContext.SaveChanges();

                var productVM = new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName
                };
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProductHub>();
                hubContext.Clients.All.update(productVM);
            }
            return Task.FromResult(0);
        }
    }
}