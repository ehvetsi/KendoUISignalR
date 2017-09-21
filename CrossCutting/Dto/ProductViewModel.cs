using System.ComponentModel;

namespace KendoUISignalR.CrossCutting.Dto
{
    public class ProductViewModel
    {
        public bool Discontinued
        {
            get;
            set;
        }

        public int ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public decimal UnitPrice
        {
            get;
            set;
        }

        public int UnitsInStock
        {
            get;
            set;
        }
    }
}