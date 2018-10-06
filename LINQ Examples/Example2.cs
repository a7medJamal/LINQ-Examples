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
    public partial class Example2 : Form
    {
        public Example2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EX2_Developer[] developers = new EX2_Developer[]
            {
                new EX2_Developer{Name="Ahmed",Language="C#",Age=20},
                                new EX2_Developer{Name="Ali",Language="C#",Age=22},
                new EX2_Developer{Name="Mohammed",Language="Java",Age=29},
                new EX2_Developer{Name="Baha",Language="IOS",Age=30},
                new EX2_Developer{Name="",Language="IOS",Age=32}

            };

            IEnumerable<string> devs =
                from d in developers
                where d.Language == comboBox1.Text
                select d.Name;
            listBox1.Items.Clear();

            foreach (string value in devs)
                listBox1.Items.Add(value);
        }
    }
}
