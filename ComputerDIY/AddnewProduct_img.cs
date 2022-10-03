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
    public partial class AddnewProduct_img : Form
    {
        Goods goods;
        public AddnewProduct_img(Goods goods)
        {
            this.goods = goods;
            InitializeComponent();
        }

        private void AddnewProduct_img_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.goods.imgLink = textBox1.Text;
            this.Close();
        }
    }
}
