using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using KendoUISignalR.Models;
using KendoUISignalR.CrossCutting.Dto;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KendoUISignalR.Hubs
{
    public class ProductHub : Hub
    {
        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            using (var db = new SampleEntities())
            {
                var user = db.UserSet
                    .SingleOrDefault(u => u.UserName == name);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = name,
                        Connection = new List<Connection>()
                    };
                    db.UserSet.Add(user);
                }

                user.Connection.Add(new Connection
                {
                    ConnectionId = Context.ConnectionId,
                    Connected = true
                });
                db.SaveChanges();
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            using (var db = new SampleEntities())
            {
                var connection = db.ConnectionSet.Find(Context.ConnectionId);
                connection.Connected = false;
                db.SaveChanges();
            }
            return base.OnDisconnected(stopCalled);
        }

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

        public void Update(ProductViewModel product, string who)
        {
            using (var db = new SampleEntities())
            {
                var user = db.UserSet.Find(who);
                if (user == null)
                {
                    Clients.Caller.showErrorMessage("Could not find that user.");
                }
                else
                {
                    db.Entry(user)
                        .Collection(u => u.Connection)
                        .Query()
                        .Where(c => c.Connected == true)
                        .Load();

                    if (user.Connection == null)
                    {
                        Clients.Caller.showErrorMessage("The user is no longer connected.");
                    }
                    else
                    {
                        foreach (var connection in user.Connection)
                        {
                            Clients.Client(connection.ConnectionId)
                                .update(product);
                        }
                    }
                }
            }
        }
    }
}