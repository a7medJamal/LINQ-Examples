﻿using System;
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
                lst.Items.Add("Product (" + item.IdProduct + ")"  );
                foreach (var order in item.o)
                    lst.Items.Add("       Qty" + order.Quantity+"- Month"+order.Month);
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
                       select new { p.IdProduct, p.Price, ord=o };

            lst.Items.Clear();
            foreach (var item in expr)
            {
                lst.Items.Add("Product (" + item.IdProduct + ")"+item.Price);
                foreach (var orders in item.ord)
                    lst.Items.Add("       Qty" + orders.Quantity + "- Month" + orders.Month);
            }
        }

        private void Exam_23_Click(object sender, EventArgs e)
        {
            var expr = from o in prodeucts
                       join y in (from c in customers
                                  from p in c.Orders
                                  select p )
           on o.IdProduct equals   y.ProductID into orders
                       select new { o.IdProduct, o.Price, ord = orders };

            lst.Items.Clear();
            foreach (var item in expr)
            {
                lst.Items.Add("Product (" + item.IdProduct + ")" + item.Price);
                foreach (var orders in item.ord)
                    lst.Items.Add("       Qty" + orders.Quantity + "- Month" + orders.Month);
            }
            #endregion
        }
    }
}
