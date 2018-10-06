using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Examples
{
    public class Product
    {
        public int IdProduct { get; set; }
        public decimal Price { get; set; }

        public Product(int IdProduct,decimal Price)
        {
            this.IdProduct = IdProduct;
            this.Price = Price;
        }
    }
}
