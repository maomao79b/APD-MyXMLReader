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
    public partial class Login: Form
    {
        Jack context = new Jack();
        public Login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            try
            {
                var result = context.P_Account.Where(a => a.Username == username && a.Password == password)
                    .Select(a => a.Status).First();
                if(int.Parse(result) == 1)// เจ้าของร้าน
                {
                    Admin admin = new Admin(this);
                    admin.Visible = true;
                    this.Visible = false;
                }
                else if (int.Parse(result) == 2)// จัดการสินค้า
                {
                    Goods goods = new Goods(this);
                    goods.Visible = true;
                    this.Visible = false;
                }else if(int.Parse(result) == 3)// ขายของ
                {
                    Shop shop = new Shop(this);
                    shop.Visible = true;
                    this.Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
