using KendoUISignalR.CrossCutting.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Interfaces
{
    public interface IProductMessage
    {
        ProductViewModel Product { get; }
    }
}