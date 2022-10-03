using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerDIY
{
    public partial class Shop : Form
    {
        Login login;
        public Shop(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Shop_Load(object sender, EventArgs e)
        {

        }

        private void Shop_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.login.Visible = true;
        }
    }
}
