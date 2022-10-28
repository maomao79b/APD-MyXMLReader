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
            if (shop.CmChkStatus == false)
            {
                try
                {
                    P_Order order = new P_Order();
                    order.Cname = "ลูกค้าทั่วไป";
                    order.OrderDate = DateTime.Now;
                    foreach (ListViewItem item in shop.listView1.Items)
                    {
                        P_OrderItem orderItem = new P_OrderItem();
                        int productId = int.Parse(item.SubItems[0].Text);
                        orderItem.OrderId = order.Id;
                        orderItem.ProductId = productId;
                        orderItem.Price = decimal.Parse(item.SubItems[3].Text);
                        orderItem.Amount = int.Parse(item.SubItems[4].Text);
                        orderItem.Type = item.SubItems[2].Text;
                        orderItem.TotalPrice = decimal.Parse(item.SubItems[5].Text);
                        context.P_OrderItem.Add(orderItem);
                        var result2 = context.P_Product.Where(p => p.Id == productId).First();
                        result2.Amount = result2.Amount - 1;
                    }
                    order.Discount = this.discount;
                    order.TotalPrice = sumPrice;
                    context.P_Order.Add(order);
                    context.SaveChanges();
                    shop.listView1.Items.Clear();
                    shop.label11.Text = "0";
                    shop.text_ID_pay.Text = "";
                    shop.text_Name_pay.Text = "";
                    shop.text_Phone_pay.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed");
                }
            }
            else
            {
                try
                {
                    P_Order order = new P_Order();
                    order.Cid = int.Parse(shop.text_ID_pay.Text);
                    order.Cname = shop.text_Name_pay.Text;
                    order.Phone = shop.text_Phone_pay.Text;
                    order.OrderDate = DateTime.Now;
                    foreach (ListViewItem item in shop.listView1.Items)
                    {
                        P_OrderItem orderItem = new P_OrderItem();
                        int productId = int.Parse(item.SubItems[0].Text);
                        orderItem.OrderId = order.Id;
                        orderItem.ProductId = productId;
                        orderItem.Price = decimal.Parse(item.SubItems[3].Text);
                        orderItem.Amount = int.Parse(item.SubItems[4].Text);
                        orderItem.Type = item.SubItems[2].Text;
                        orderItem.TotalPrice = decimal.Parse(item.SubItems[5].Text);
                        context.P_OrderItem.Add(orderItem);
                        var result2 = context.P_Product.Where(p => p.Id == productId).First();
                        result2.Amount = result2.Amount - 1;
                    }
                    order.Discount = discount;
                    order.TotalPrice = sumPrice;
                    context.P_Order.Add(order);
                    context.SaveChanges();
                    shop.listView1.Items.Clear();
                    shop.label11.Text = "0";
                    shop.text_ID_pay.Text = "";
                    shop.text_Name_pay.Text = "";
                    shop.text_Phone_pay.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed");
                }
            }
            MessageBox.Show("Success");
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
    }
}
