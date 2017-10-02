using KendoUISignalR.CrossCutting.Dto;
using KendoUISignalR.Hubs;
using KendoUISignalR.Models;
using MassTransit;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unifique.ServiceBroker.Messaging.Interfaces;

namespace RabbitMQMassTransitSubscriber
{
    public class ProductConsumer : IConsumer<IProductMessage>
    {
        public Task Consume(ConsumeContext<IProductMessage> context)
        {
            //using (var dbContext = new SampleEntities())
            //{
            //    var product = dbContext.Products.FirstOrDefault(c => c.ProductID == context.Message.Product.ProductID);
            //    product.ProductName = "Alterado" + DateTime.Now.Millisecond;
            //    dbContext.SaveChanges();

            //    var productVM = new ProductViewModel
            //    {
            //        ProductID = product.ProductID,
            //        ProductName = product.ProductName
            //    };

            //    const string url = @"http://localhost:51737/";
            //    var connection = new HubConnection(url);
            //    var hub = connection.CreateHubProxy("productHub");
            //    connection.Start().Wait();

            //    hub.Invoke("update", productVM).Wait();
            //    Console.WriteLine($"Atualizado produto: {productVM.ProductID}");
            //}
            Console.WriteLine($"Recebido: {context.Message}");

            return Task.FromResult(0);
        }
    }
}