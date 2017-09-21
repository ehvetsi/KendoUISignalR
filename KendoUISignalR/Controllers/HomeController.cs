using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoUISignalR.CrossCutting.Dto;
using KendoUISignalR.Hubs;
using KendoUISignalR.Models;
using KendoUISignalR.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KendoUISignalR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Customers()
        {
            return View();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartUpdateCustomers()
        {
            Timer timer = new Timer(o => UpdateCustomers(), null, 0, 2000);
            return null;
        }

        public async Task<ActionResult> StartUpdateProducts(int productId)
        {
            await new ProductService().ProductQueuer(new Services.Messages.ProductMessage()
            {
                Product = new ProductViewModel()
                {
                    ProductID = productId
                }
            });
            return new HttpStatusCodeResult(200);
        }

        private static void UpdateCustomers()
        {
            using (var context = new SampleEntities())
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomerID.Equals("ALFKI"));
                customer.ContactName = "Alterado" + DateTime.Now.Millisecond;
                context.SaveChanges();

                var customerVM = new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle
                };
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
                hubContext.Clients.All.update(customerVM);
            }
        }

        private static void UpdateProducts()
        {
            //using (var context = new SampleEntities())
            //{
            //    var product = context.Products.FirstOrDefault(c => c.ProductID == 1);
            //    product.ProductName = "Alterado" + DateTime.Now.Millisecond;
            //    context.SaveChanges();

            //    var productVM = new ProductViewModel
            //    {
            //        ProductID = product.ProductID,
            //        ProductName = product.ProductName
            //    };
            //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProductHub>();
            //    hubContext.Clients.All.update(productVM);
            //}
        }
    }
}