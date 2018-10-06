using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Examples
{
    public class Order
    {
        public int Quantity { get; set; }
        public bool Shipped { get; set; }
        public int Month { get; set; }
        public Product Products { get; set; }
        
        public Order(int quantity,bool shipped,int month,Product product)
        {
            this.Quantity = quantity;
            this.Shipped = shipped;
            this.Month = month;
            this.Products = product;
        }
    }
}
