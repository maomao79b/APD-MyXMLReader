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
    public partial class PayBill : Form
    {
        Shop shop;
        decimal sumPrice;
        string discount;
        Jack context = new Jack();
        public PayBill(Shop shop, decimal sumPrice, string discount)
        {
            this.shop = shop;
            this.sumPrice = sumPrice;
            this.discount = discount;
            InitializeComponent();
        }

        private void PayBill_Load(object sender, EventArgs e)
        {
            textBox1.Text = sumPrice.ToString();
            if (discount != "")
            {
                label3.Text = discount;
                label5.Text = "";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("กรุณากรอกจำนวนเงิน");
            }
            else
            {
                try
                {
                    decimal receive = decimal.Parse(textBox2.Text);
                    if (sumPrice > receive)
                    {
                        MessageBox.Show("จำนวนเงินไม่ถูกต้อง");
                    }
                    else
                    {
                        textBox3.Text = (receive-sumPrice).ToString();
                        tabControl1.SelectedIndex = 1;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("เป็นตัวเลขเท่านั้น");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            shop.ChkPay = true;
            MessageBox.Show("Success");
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
    }
}
