using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoUISignalR.Hubs;
using KendoUISignalR.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public ActionResult StartUpdateProducts()
        {
            Timer timer = new Timer(o => UpdateProducts(), null, 0, 2000);
            return null;
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
            using (var context = new SampleEntities())
            {
                var product = context.Products.FirstOrDefault(c => c.ProductID == 1);
                product.ProductName = "Alterado" + DateTime.Now.Millisecond;
                context.SaveChanges();

                var productVM = new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName
                };
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProductHub>();
                hubContext.Clients.All.update(productVM);
            }
        }
    }
}