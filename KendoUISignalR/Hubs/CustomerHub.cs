using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using KendoUISignalR.Models;

namespace KendoUISignalR.Hubs
{
    public class CustomerHub : Hub
    {
        public IEnumerable<CustomerViewModel> Read()
        {
            using (var context = new SampleEntities())
            {
                IEnumerable<Customers> products = context.Customers.ToList();
                var result = products.Select(c => new CustomerViewModel
                {
                    CustomerID = c.CustomerID,
                    ContactName = c.ContactName,
                    ContactTitle = c.ContactTitle
                });
                return result;
            }
        }

        public void Update(CustomerViewModel customer)
        {
            Clients.Others.update(customer);
        }
    }
}