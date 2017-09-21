using CrossCutting.Interfaces;
using KendoUISignalR.CrossCutting.Dto;
using KendoUISignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoUISignalR.Services.Messages
{
    public class ProductMessage : IProductMessage
    {
        public ProductViewModel Product { get; set; }
    }
}