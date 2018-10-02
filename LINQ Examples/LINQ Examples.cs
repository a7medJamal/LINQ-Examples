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
                
            }
            else
            {
                MessageBox.Show("Selected Path Firestly");
            }
        }
    }
}
