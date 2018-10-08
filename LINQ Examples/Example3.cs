using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ_Examples
{
    public partial class Example3 : Form
    {
        Customer[] customers;
        Product[] prodeucts;
        public Example3()
        {
            InitializeComponent();

        }

        private void Example3_Load(object sender, EventArgs e)
        {
            customers = new Customer[]
            {
                new Customer("ahmed","cairo",EnumCountries.Egypt,new Order[]{ new Order (3,true,1,1),
                new Order(2,false,2,2),
                  new Order(5,true,9,3),
                  new Order(96,true,3,4),
                  new Order(7,false,4,5),
                  new Order(9,true,5,6) }) ,

                     new Customer("ali","6 october",EnumCountries.Egypt,new Order[]{ new Order (3,true,1,1),
                new Order(2,false,2,2),
                  new Order(15,true,9,3),
                  new Order(6,true,3,4),
                  new Order(7,false,4,5),
                  new Order(93,true,5,6) } ) ,

                          new Customer("mohammed","ahmadi",EnumCountries.Kuwait,new Order[]{ new Order (3,true,1,1),
                new Order(2,false,2,2),
                  new Order(5,true,9,3),
                  new Order(9,false,5,6) } ) ,

                             new Customer("basem","Salimiya",EnumCountries.Kuwait,new Order[]{ new Order (60,true,1,1),
                  new Order(50,true,9,3),
                  new Order(62,true,3,4),
                  new Order(99,false,5,6) } )
            };

            prodeucts = new Product[]

            {
                new Product (1,50),
                new Product (2,60),
                new Product (3,70),
                  new Product (4,70),
                    new Product (5,70),
                      new Product (6,70),
                        new Product (7,70)
            };
        }

        #region Where Operator in LINQ
        private void Exam_1_Click(object sender, EventArgs e)
        {
            var expr1 =
                from c in customers
                where c.Country == EnumCountries.Egypt
                select new { c.Name, c.City };
            lst.Items.Clear();
            foreach (var x in expr1)
                lst.Items.Add(x.Name + "(" + x.City + ")");

        }

        private void Exam_2_Click(object sender, EventArgs e)
        {
            var expr2 =
                customers
                .Where((c, index) => (c.Country == EnumCountries.Egypt && index >= 0))
                .Select(c => c.Name);
            lst.Items.Clear();
            foreach (var x in expr2)
                lst.Items.Add(x);
        }

        private void Exam_3_Click(object sender, EventArgs e)
        {
            int start = 1;
            int end = 5;
            var expr3 =
                customers
                .Where((c, index) => ((index >= start) && (index < end)))
                .Select(c => c.Name);
            lst.Items.Clear();
            foreach (var x in expr3)
                lst.Items.Add(x);
        }

        #endregion

        #region Projection Operators (Select)
        private void Exam_4_Click(object sender, EventArgs e)
        {
            var expr1 = customers.Select(c => c.Name);
            lst.Items.Clear();
            foreach (var x in expr1)
                lst.Items.Add(x);
        }

        private void Exam_5_Click(object sender, EventArgs e)
        {
            var expr2 = customers.Select(c => new { c.Name, c.City });
            lst.Items.Clear();
            foreach (var x in expr2)
                lst.Items.Add(x.Name + "(" + x.City + ")");
        }

        private void Exam_6_Click(object sender, EventArgs e)
        {
            var orders = customers
                  .Where(c => c.Country == EnumCountries.Egypt)
                  .Select(c => c.Orders);
            lst.Items.Clear();
            foreach (var x in orders)
                foreach (var items in x)
                    lst.Items.Add(items.Quantity + "(" + items.Shipped + ")");
        }
        #endregion

        #region Projection Operators (Select Many)
        private void Exam_7_Click(object sender, EventArgs e)
        {
            IEnumerable<Order> orders = customers
                .Where(c => c.Country == EnumCountries.Egypt)
                .SelectMany(c => c.Orders);
            lst.Items.Clear();
            foreach (var x in orders)
                lst.Items.Add(x.Quantity + "(" + x.Shipped + ")");
        }

        private void Exam_8_Click(object sender, EventArgs e)
        {
            IEnumerable<Order> orders =
                from c in customers
                where c.Country == EnumCountries.Egypt
                from o in c.Orders
                select o;
            lst.Items.Clear();
            foreach (var x in orders)
                lst.Items.Add(x.Quantity + "(" + x.Shipped + ")");
        }

        private void Exam_9_Click(object sender, EventArgs e)
        {
            var items = customers
                .Where(c => c.Country == EnumCountries.Egypt)
                .SelectMany(c => c.Orders, (c, o) => new { o.Quantity, o.Shipped });
            lst.Items.Clear();
            foreach (var x in items)
                lst.Items.Add(x.Quantity + "(" + x.Shipped + ")");
        }


        private void Exam_10_Click(object sender, EventArgs e)
        {
            var orders =
                from c in customers
                .Where(c => c.Country == EnumCountries.Egypt)
                from o in c.Orders
                select new { o.Quantity, o.Shipped };
            lst.Items.Clear();
            foreach (var x in orders)
                lst.Items.Add(x.Quantity + "(" + x.Shipped + ")");
        }
        #endregion

        #region Ordering OPerators (OrderBy)
        private void Exam_11_Click(object sender, EventArgs e)
        {
            var expr =
                from x in customers
                where x.Country == EnumCountries.Egypt
                orderby x.Name descending
                select new { x.Name, x.City };
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add(c.Name + "(" + c.City + ")");
        }
        #endregion

        #region Ordering OPerators (OrderBy Descending)
        private void Exam_12_Click(object sender, EventArgs e)
        {
            var expr = customers
                .Where(x => x.Country == EnumCountries.Egypt)
                .OrderByDescending(x => x.Name)
                 .Select(x => new { x.Name, x.City });
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add(c.Name + "(" + c.City + ")");
        }
        #endregion

        #region Ordering OPerators (ThenBy ,TenBy Descending,Revers)
        private void Exam_13_Click(object sender, EventArgs e)
        {
            var expr = customers
                .Where(c => c.Country == EnumCountries.Egypt)
                .OrderByDescending(c => c.Name)
                .ThenBy(c => c.City)
                .Select(c => new { c.Name, c.City });
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add(c.Name + "(" + c.City + ")");
        }

        private void Exam_14_Click(object sender, EventArgs e)
        {
            //this LINQ Operators (why ... because start from *** from** condestion)
            var expr =
                from c in customers
                where c.Country == EnumCountries.Egypt
                orderby c.Name descending, c.City
                select new { c.Name, c.City };
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add(c.Name + "(" + c.City + ")");
        }

        private void Exam_15_Click(object sender, EventArgs e)
        {
            var expr =
                from c in customers
                from o in c.Orders
                orderby o.Month descending
                select o;
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add(c.Quantity + "(" + c.Month + ")");
        }

        private void Exam_16_Click(object sender, EventArgs e)
        {
            var expr = customers
                .Where(c => c.Country == EnumCountries.Egypt)
                .OrderByDescending(c => c.Name)
                .ThenBy(c => c.City)
                .Select(c => new { c.Name, c.City }).Reverse();
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add(c.Name + "(" + c.City + ")");
        }
        #endregion

        #region Grouping operators 
        private void Exam_17_Click(object sender, EventArgs e)
        {
            var expr = customers.GroupBy(x => x.Country);
            lst.Items.Clear();
            foreach (IGrouping<EnumCountries, Customer> countries in expr)
            {
                lst.Items.Add(countries.Key);
                foreach (var cust in countries)
                    lst.Items.Add("      " + cust.Name + "(" + cust.City + ")");
            }
        }

        private void Exam_18_Click(object sender, EventArgs e)
        {
            // this by LINQ
            var expr =
                from c in customers
                group c by new { c.Country, c.Name };

            lst.Items.Clear();
            foreach (var countries in expr)
            {
                lst.Items.Add(countries.Key);
                foreach (var cust in countries)
                    lst.Items.Add("      " + cust.Name + "(" + cust.City + ")");
            }

        }
        #endregion

        #region Join Operator
        private void Exam_19_Click(object sender, EventArgs e)
        {
            var expr = customers
                .SelectMany(c => c.Orders)
                .Join(prodeucts, o => o.ProductID, p => p.IdProduct,
                (o, p) => new { o.Month, o.Quantity, p.IdProduct, o.Shipped });
            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add("Shipped (" + c.Shipped + ")" + "IdProduct (" + c.IdProduct + ")" + "Month (" + c.Month + ")" + "Quantity (" + c.Quantity + ")");
        }

        private void Exam_20_Click(object sender, EventArgs e)
        {
            var expr =
               from c in customers
               from o in c.Orders
               join p in prodeucts
               on o.ProductID equals p.IdProduct
               select new { o.ProductID, o.Quantity, o.Shipped, p.IdProduct, o.Month };

            lst.Items.Clear();
            foreach (var c in expr)
                lst.Items.Add("Shipped (" + c.Shipped + ")" + "IdProduct (" + c.IdProduct + ")" + "Month (" + c.Month + ")" + "Quantity (" + c.Quantity + ")");
        }
        #endregion

        #region Group Join & SubQuery
        private void Exam_21_Click(object sender, EventArgs e)
        {
            var expr = prodeucts
                .GroupJoin(customers.SelectMany(c => c.Orders),
                p => p.IdProduct,
                o => o.ProductID,
                (p, o) => new { p.IdProduct, o });
            lst.Items.Clear();
            foreach (var item in expr)
            {
                lst.Items.Add("Product (" + item.IdProduct + ")");
                foreach (var order in item.o)
                    lst.Items.Add("       Qty" + order.Quantity + "- Month" + order.Month);
            }
        }

        private void Exam_22_Click(object sender, EventArgs e)
        {
            var order =
                from c in customers
                from o in c.Orders
                select o;
            var expr = from p in prodeucts
                       join o in order
                       on p.IdProduct equals o.ProductID into o
                       select new { p.IdProduct, p.Price, ord = o };

            lst.Items.Clear();
            foreach (var item in expr)
            {
                lst.Items.Add("Product (" + item.IdProduct + ")" + item.Price);
                foreach (var orders in item.ord)
                    lst.Items.Add("       Qty" + orders.Quantity + "- Month" + orders.Month);
            }
        }

        private void Exam_23_Click(object sender, EventArgs e)
        {
            var expr = from o in prodeucts
                       join y in (from c in customers
                                  from p in c.Orders
                                  select p)
           on o.IdProduct equals y.ProductID into orders
                       select new { o.IdProduct, o.Price, ord = orders };

            lst.Items.Clear();
            foreach (var item in expr)
            {
                lst.Items.Add("Product (" + item.IdProduct + ")" + item.Price);
                foreach (var orders in item.ord)
                    lst.Items.Add("       Qty" + orders.Quantity + "- Month" + orders.Month);
            }

        }
        #endregion

        #region Distinct
        private void Exam_24_Click(object sender, EventArgs e)
        {
            var expr = customers
                .SelectMany(c => c.Orders)
                .Join(prodeucts, o => o.ProductID, p => p.IdProduct, (o, p) => p).Distinct();
            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID Product" + "_" + x.IdProduct + "-" + "Price" + "_" + x.Price);

            }
        }

        private void Exam_25_Click(object sender, EventArgs e)
        {
            var expr = (from c in customers
                        from o in c.Orders
                        join p in prodeucts
                        on o.ProductID equals p.IdProduct
                        select p).Distinct();
            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID Product" + "_" + x.IdProduct + "-" + "Price" + "_" + x.Price);
            }
        }
        #endregion
        #region Union
        private void Exam_26_Click(object sender, EventArgs e)
        {
            var expr = customers[1].Orders.Union(customers[2].Orders).Distinct();
            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID Product" + "_" + x.ProductID + "-" + "Quantity" + "_" + x.Quantity);

            }
        }

        private void Exam_27_Click(object sender, EventArgs e)
        {
            var expr =
                (from c in customers
                 from o in c.Orders
                 where c.Country == EnumCountries.Egypt
                 select o).Union(from c in customers
                                 from o in c.Orders
                                 where c.Country == EnumCountries.Kuwait
                                 select o);

            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID Product" + "_" + x.ProductID + "-" + "Quantity" + "_" + x.Quantity);

            }

        }
        #endregion

        #region Intersect 
        //this like match between two items
        private void Exam_28_Click(object sender, EventArgs e)
        {
            var expr =
              (from c in customers
               from o in c.Orders
               where c.Country == EnumCountries.Egypt
               select o).Intersect(from c in customers
                                   from o in c.Orders
                                   where c.Country == EnumCountries.Kuwait
                                   select o);
            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID Product" + "_" + x.ProductID + "-" + "Quantity" + "_" + x.Quantity);
            }

        }
        #endregion

        #region Except
        private void Exam_35_Click(object sender, EventArgs e)
        {
            var expr =
              (from c in customers
               from o in c.Orders
               select o).Except(from c in customers
                                from o in c.Orders
                                where c.City == "Cairo"
                                select o);
            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID Product" + "_" + x.ProductID + "-" + "Quantity" + "_" + x.Quantity);
            }
        }
        #endregion

        #region Count ,LongCount
        private void Exam_29_Click(object sender, EventArgs e)
        {
            // you can use LongCount for big numbers
            var expr = from c in customers
                       select new { c.Name, c.City, c.Country, OrderCount = c.Orders.Count() };
            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID " + "_" + x.Name + "-" + "Order Count" + "_" + x.OrderCount);
            }
        }
        #endregion

        #region Sum 
        private void Exam_30_Click(object sender, EventArgs e)
        {
            int[] values = { 1, 5, 98, 12 };
            int total = values.Sum();
            lst.Items.Clear();
            lst.Items.Add("Sum=" + total);
        }

        private void Exam_31_Click(object sender, EventArgs e)
        {
            var orders =
                from c in customers
                from o in c.Orders
                join p in prodeucts on o.ProductID equals p.IdProduct
                select new { c.Name, Amount = o.Quantity * p.Price };
            var expr = from c in customers
                       join o in orders on c.Name equals o.Name into CustOrders
                       select new { c.Name, Amount = CustOrders.Sum(o => o.Amount) };
            var expr2 = orders.Union(expr);

            lst.Items.Clear();
            foreach (var x in expr2)
            {
                lst.Items.Add("ID " + "_" + x.Name + "-" + "Total=" + "_" + x.Amount);
            }


        }
        #endregion

        #region Neasted Query With Sum
        private void Exam_32_Click(object sender, EventArgs e)
        {
            var expr = from c in customers
                       join o in (from c in customers
                                  from o in c.Orders
                                  join p in prodeucts on o.ProductID equals p.IdProduct
                                  select new { c.Name, Amount = o.Quantity * p.Price }) on c.Name equals o.Name
                               into CustomersWithOrders

                       select new { c.Name, totalAmount = CustomersWithOrders.Sum(o => o.Amount) };

            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID " + "_" + x.Name + "-" + "Order Count" + "_" + x.totalAmount);
            }
        }
        #endregion

        #region Min
        private void Exam_33_Click(object sender, EventArgs e)
        {
            var expr = (from c in customers
                        from o in c.Orders
                        select o.Quantity).Min();
            lst.Items.Clear();
            lst.Items.Add("Min Quantity=" + expr);

        }

        private void Exam_34_Click(object sender, EventArgs e)
        {
            var expr = (from c in customers
                        from o in c.Orders
                        select new { o.Quantity }).Min(o => o.Quantity);
            lst.Items.Clear();
            lst.Items.Add("Max Quantity=" + expr);
        }
        #endregion

        #region Max
        private void Exam_36_Click(object sender, EventArgs e)
        {
            var expr = (from c in customers
                        from o in c.Orders
                        select new { o.Quantity }).Max(o => o.Quantity);
            lst.Items.Clear();
            lst.Items.Add("Max Quantity=" + expr);
        }
        #endregion

        #region Average
        private void Exam_37_Click(object sender, EventArgs e)
        {
            var expr = (from p in prodeucts
                        select new { p.Price }).Average(p => p.Price);
            lst.Items.Clear();
            lst.Items.Add("Average Quantity=" + expr);
        }

        private void Exam_40_Click(object sender, EventArgs e)
        {
            var expr = from c in customers
                       join o in (from c in customers
                                  from o in c.Orders
                                  join p in prodeucts on o.ProductID equals p.IdProduct
                                  select new { c.Name, orderamount = o.Quantity * p.Price }
                                    ) on c.Name equals o.Name
                                    into CustWithOrders
                       select new { c.Name, AvargAmount = CustWithOrders.Average(o => o.orderamount) };

            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add(x.Name + "(" + x.AvargAmount + ")");
            }
        }
        #endregion
        #region Aggregate
        private void Exam_38_Click(object sender, EventArgs e)
        {
            var expr = from p in prodeucts
                       join o in (from c in customers
                                  from o in c.Orders
                                  join p in prodeucts on o.ProductID equals p.IdProduct
                                  select new { p.IdProduct, OrdersAmount = o.Quantity * p.Price }) on p.IdProduct equals o.IdProduct into orders
                       select new { p.IdProduct, TotalAmount = orders.Aggregate(0m, (a, o) => a += o.OrdersAmount) };

            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("ID" + x.IdProduct + "-" + "(" + x.TotalAmount + ")");
            }
        }

        
        private void Exam_41_Click(object sender, EventArgs e)
        {
            var expr = from c in customers
                       join o in (from c in customers
                                  from o in c.Orders
                                  join p in prodeucts on o.ProductID equals p.IdProduct
                                  select new { c.Name, p.IdProduct, OrdersAmount = o.Quantity * p.Price }) on c.Name equals o.Name into orders
                       select new { c.Name, MaxOrderAmount = orders.Aggregate( (t, s) =>( t.OrdersAmount > s.OrdersAmount) ? t : s  ).OrdersAmount  };

            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("Name" + x.Name + "-" + "(" + x.MaxOrderAmount + ")");
            }
        }

        private void Exam_39_Click(object sender, EventArgs e)
        {
            var expr = from c in customers
                       join o in (from c in customers
                                  from o in c.Orders
                                  join p in prodeucts on o.ProductID equals p.IdProduct
                                  select new { c.Name,o.Month, p.IdProduct, OrdersAmount = o.Quantity * p.Price }) on c.Name equals o.Name into orders
                       select new { c.Name, MaxOrder = orders.Aggregate(new { Amount = 0m, month = "" }, 
                       (t, s) =>(t.Amount > s.OrdersAmount) ? t : new { Amount = s.OrdersAmount, month = s.Month.ToString() }) } ;

            lst.Items.Clear();
            foreach (var x in expr)
            {
                lst.Items.Add("Name-" + x.Name + "Max Order-" + "(" + x.MaxOrder.month + ")");
            }
        }
        #endregion
    }



}
