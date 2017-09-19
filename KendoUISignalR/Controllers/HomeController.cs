using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoUISignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUISignalR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (var northwind = new SampleEntities())
            {
                IQueryable<Products> products = northwind.Products;
                // Convert the Product entities to ProductViewModel instances
                DataSourceResult result = products.ToDataSourceResult(request, product => new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                });
                return Json(result);
            }
        }
    }
}