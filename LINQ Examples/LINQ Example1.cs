using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ_Examples
{
    public partial class LINQ_Examples : Form
    {
        public LINQ_Examples()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
         if(  FBD.ShowDialog()==DialogResult.OK)
            {
                txtPass.Text = FBD.SelectedPath;
            }
        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            if(txtPass.Text !="")
            {
                // linq to object
                DirectoryInfo dirInfo = new DirectoryInfo(txtPass.Text);

                var selectFiles = from f in dirInfo.GetFiles()
                                  where f.Length > 10
                                  orderby f.Length descending
                                  select f.Name;
                foreach(string name in selectFiles)
                fileList.Items.Add(name);
            }
            else
            {
                MessageBox.Show("Selected Path Firestly");
            }
        }
    }
}
