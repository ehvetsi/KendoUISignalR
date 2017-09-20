using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using KendoUISignalR.Models;

namespace KendoUISignalR.Hubs
{
    public class ProductHub : Hub
    {
        public IEnumerable<ProductViewModel> Read()
        {
            using (var context = new SampleEntities())
            {
                IEnumerable<Products> products = context.Products.ToList();
                var result = products.Select(c => new ProductViewModel
                {
                    ProductID = c.ProductID,
                    ProductName = c.ProductName,
                });
                return result;
            }
        }

        public void Update(ProductViewModel product)
        {
            Clients.Others.update(product);
        }
    }
}