using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;

namespace ComputerDIY
{
    public partial class Shop : Form
    {
        Login login;
        Jack context = new Jack();
        public string readQRCode;
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

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pProductBindingSource.DataSource = context.P_Product.ToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tx1 = textBox1.Text;

            var result = from pro in context.P_Product
                         where pro.Id.ToString().Contains(tx1) ||
                         pro.Name.Contains(tx1) ||
                         pro.Detail.Contains(tx1) ||
                         pro.Type.Contains(tx1)
                         select pro;
            pProductBindingSource.DataSource = result.ToList();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string tx9 = textBox9.Text;

            var result = from pro in context.P_Product
                         where pro.Type.Contains(tx9)
                         select pro;
            pProductBindingSource.DataSource = result.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string tx7 = textBox7.Text;

            var result = from pro in context.P_Product
                         where pro.Detail.Contains(tx7)
                         select pro;
            pProductBindingSource.DataSource = result.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string tx6 = textBox6.Text;

            var result = from pro in context.P_Product
                         where pro.Name.Contains(tx6)
                         select pro;
            pProductBindingSource.DataSource = result.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string tx5 = textBox5.Text;

            var result = from pro in context.P_Product
                         where pro.Id.ToString().Contains(tx5)
                         select pro;
            pProductBindingSource.DataSource = result.ToList();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(textBox3.Text);
            var f = context.P_Product.Where(p => p.Id == id).First();
            pictureBox.Image = LoadImage(f.Image);
        }

        private Image LoadImage(string url)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                Bitmap bmp = new Bitmap(streamResponse);
                streamResponse.Dispose();
                return bmp;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReadQRCode qrcode = new ReadQRCode(this);
            qrcode.ShowDialog();
            Console.WriteLine(this.readQRCode);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.SelectedItems[0].Remove();
            }
            catch (Exception)
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.SelectedItems[0].SubItems[4].Text = textBox11.Text;
                decimal total = decimal.Parse(textBox11.Text) * decimal.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                listView1.SelectedItems[0].SubItems[5].Text = total.ToString();
                label11.Text = calculateTotal(listView1.Items).ToString();
            }
            catch (Exception)
            {
                
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox11.Text = listView1.SelectedItems[0].SubItems[4].Text;
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                var f = context.P_Product.Where(p => p.Id == id).First();
                textBox8.Text = f.Detail;
                pictureBox1.Image = LoadImage(f.Image);
            }
            catch (Exception)
            {

            }
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
            
        }
        
        public double calculateTotal(ListView.ListViewItemCollection items)
        {
            double total = 0;
            foreach(ListViewItem item in items)
                total += double.Parse(item.SubItems[5].Text);
            return total;
        }

        private void btnAddId_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox_addIdProduct.Text);
                var f = context.P_Product.Where(p => p.Id == id).First();
                string[] items = new string[]
                {
                    textBox_addIdProduct.Text,
                    f.Name,
                    f.Type,
                    f.Price.ToString(),
                    "1",
                    f.Price.ToString()
                };
                listView1.Items.Add(new ListViewItem(items));
                label11.Text = calculateTotal(listView1.Items).ToString();
            }
            catch (Exception)
            {
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string nameProduct = textBox3.Text;
            //var result2 = context.AmazonProducts.Where(p => p.ProductName == nameProduct).First();
            P_Order order = new P_Order();
            order.Cid = int.Parse(text_ID_pay.Text);
            order.Cname = text_Name_pay.Text;
            order.Phone = text_Phone_pay.Text;
            order.OrderDate = DateTime.Now;
            //order.TotalPrice = decimal.Parse(label11.Text);
            //context.P_Order.Add(order);
            //Console.WriteLine(order.Id.ToString());
            int countid1 = 0;
            int countid2 = 0;
            decimal totalPrice1 = 0;
            decimal totalPrice2 = 0;
            var promotion = context.P_Promotion.First();
            foreach (ListViewItem item in listView1.Items)
            {
                P_OrderItem orderItem = new P_OrderItem();
                int productId = int.Parse(item.SubItems[0].Text);
                orderItem.OrderId = order.Id;
                orderItem.ProductId = productId;
                //Console.WriteLine(item.SubItems[3].Text);
                orderItem.Price = decimal.Parse(item.SubItems[3].Text);
                orderItem.Amount = int.Parse(item.SubItems[4].Text);
                orderItem.Type = item.SubItems[2].Text;
                orderItem.TotalPrice = decimal.Parse(item.SubItems[5].Text);
                context.P_OrderItem.Add(orderItem);
                //Console.WriteLine(item.SubItems[0].Text);
                try
                {
                    if(promotion.Product_1 == item.SubItems[0].Text)
                    {
                        countid1 += int.Parse(item.SubItems[4].Text);
                        totalPrice1 += decimal.Parse(item.SubItems[5].Text);
                    }
                    else if(promotion.Product_2 == item.SubItems[0].Text)
                    {
                        countid2 += int.Parse(item.SubItems[4].Text);
                        totalPrice2 += decimal.Parse(item.SubItems[5].Text);
                    }

                }
                catch (Exception)
                {

                }
                var result = context.P_Product.Where(p => p.Id == productId).First();
                result.Amount = result.Amount - 1;
            }
            string textDis="";
            decimal discount=0;
            if (countid1 >= 1 && countid2 >= 1)
            {
                int amountPack;
                decimal newtotal;
                if (countid1 == countid2)
                {
                    amountPack = countid1;
                    discount = (totalPrice1 + totalPrice2) * (decimal)0.1;
                }
                else
                {
                    if (countid1 > countid2)
                    {
                        amountPack = countid2;
                        newtotal = (totalPrice1 / countid1) * amountPack;
                        discount = (newtotal + totalPrice2) * (decimal)0.1;
                    }
                    else
                    {
                        amountPack = countid1;
                        newtotal = (totalPrice2 / countid2) * amountPack;
                        discount = (newtotal + totalPrice2) * (decimal)0.1;
                    }
                }
                textDis = "ลด 10% ทั้งหมด "+amountPack+"คู่";
                order.Discount = textDis;
            }
            order.TotalPrice = decimal.Parse(label11.Text) - discount; ;
            context.P_Order.Add(order);
            context.SaveChanges();
            listView1.Items.Clear();
            label11.Text = "0";
            text_ID_pay.Text = "";
            text_Name_pay.Text = "";
            text_Phone_pay.Text = "";
            //textBox8 = "";

            MessageBox.Show("Success");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox3.Text);
                var f = context.P_Product.Where(p => p.Id == id).First();
                string[] items = new string[]
                {
                    textBox3.Text,
                    f.Name,
                    f.Type,
                    f.Price.ToString(),
                    "1",
                    f.Price.ToString()
                };
                listView1.Items.Add(new ListViewItem(items));
                label11.Text = calculateTotal(listView1.Items).ToString();
                MessageBox.Show("เพิ่มสำเร็จ");
            }
            catch (Exception)
            {

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
