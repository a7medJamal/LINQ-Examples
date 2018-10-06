using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Examples
{
    class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
        public EnumCountries Country { get; set; }

        public Order[] Orders { set; get; }

        public Customer(string name,string city,EnumCountries country,Order[] orders)
        {
            this.Name = name;
            this.City = city;
            this.Country = country;
            this.Orders = orders;
        }
        
    }
}
