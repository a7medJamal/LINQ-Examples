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
                new Customer("ahmed","cairo",EnumCountries.Egypt,new Order[]{ new Order (3,true,1,new Product (1,100)),
                new Order(2,false,2,new Product (2,1030)),
                  new Order(5,true,9,new Product (3,1070)),
                  new Order(96,true,3,new Product (4,1900)),
                  new Order(7,false,4,new Product (5,150)),
                  new Order(9,true,5,new Product (6,110)) } ) ,

                     new Customer("ali","6 october",EnumCountries.Egypt,new Order[]{ new Order (3,true,1,new Product (1,100)),
                new Order(2,false,2,new Product (2,130)),
                  new Order(15,true,9,new Product (3,170)),
                  new Order(6,true,3,new Product (4,190)),
                  new Order(7,false,4,new Product (5,150)),
                  new Order(93,true,5,new Product (6,1010)) } ) ,

                          new Customer("mohammed","ahmadi",EnumCountries.Kuwait,new Order[]{ new Order (3,true,1,new Product (1,100)),
                new Order(2,false,2,new Product (2,100)),
                  new Order(5,true,9,new Product (3,180)),
                  new Order(9,false,5,new Product (6,150)) } ) ,

                             new Customer("basem","Salimiya",EnumCountries.Kuwait,new Order[]{ new Order (60,true,1,new Product (1,100)),
                  new Order(50,true,9,new Product (3,180)),
                  new Order(62,true,3,new Product (4,140)),
                  new Order(99,false,5,new Product (6,150)) } ) 
            };

            prodeucts = new Product[]

            {
                new Product (10,50),
                new Product (11,60),
                new Product (12,70)
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
                lst.Items.Add(x.Name +"("+x.City+")");
        }
    
        private void Exam_6_Click(object sender, EventArgs e)
        {
            var orders = customers
                  .Where(c => c.Country == EnumCountries.Egypt)
                  .Select(c => c.Orders);
            lst.Items.Clear();
            foreach (var x in orders)
                foreach(var items in x)
                lst.Items.Add(items.Quantity+"("+items.Shipped+")");
        }
        #endregion
        private void Exam_7_Click(object sender, EventArgs e)
        {

        }
    }
}
