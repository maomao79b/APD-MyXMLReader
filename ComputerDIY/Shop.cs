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
        bool IdChkPay = false;
        bool SlChkClick = false;
        bool CmChkClick = false;
        //public bool CmChkStatus = false;
        public bool ChkPay = false;
        public Shop(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Shop_Load(object sender, EventArgs e)
        {
            try
            {
                var result = context.P_Product.ToList();
                foreach (var item in result)
                {
                    if (comboBox1.Items.IndexOf(item.Type) < 0)
                        comboBox1.Items.Add(item.Type);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("เกิดข้อผิดพลาด");
            }
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
            if (SlChkClick)
            {
                if(textBox11.Text == "")
                {
                    MessageBox.Show("กรุณาใส่จำนวนสินค้า");
                }
                try
                {
                    int amount = int.Parse(textBox11.Text);
                    textBox11.Text = amount.ToString();
                    if(amount == 0)
                    {
                        MessageBox.Show("จำนวนไม่ถูกต้อง");
                    }
                    else
                    {
                        int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                        var result = context.P_Product.Where(pd => pd.Id == id).First();
                        if(amount > result.Amount)
                        {
                            MessageBox.Show("จำนวนสินค้าไม่เพียงพอ สินค้า ID: "+id+" มีจำนวน "+result.Amount+" ชิ้น");
                        }
                        else
                        {
                            listView1.SelectedItems[0].SubItems[4].Text = amount.ToString();
                            decimal total = decimal.Parse(textBox11.Text) * decimal.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                            listView1.SelectedItems[0].SubItems[5].Text = total.ToString();
                            label11.Text = calculateTotal(listView1.Items).ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("กรุณาใส่เป็นตัวเลขเท่านั้น");
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการเปลี่ยนแปลง");
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
                SlChkClick = true;
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
                var result2 = context.P_Product.Where(pd => pd.Id == id).First();
                if(result2.Amount == 0)
                {
                    MessageBox.Show("สินค้าหมด");
                }
                else
                {
                    var result = listView1.FindItemWithText(textBox_addIdProduct.Text);
                    if(result != null)
                    {
                        int ChkAmoun = int.Parse(result.SubItems[4].Text) + 1;
                        if (ChkAmoun > result2.Amount)
                        {
                            MessageBox.Show("จำนวนสินค้าไม่เพียงพอ สินค้า ID: " + id + " มีจำนวน " + result2.Amount + " ชิ้น");
                        }
                        else
                        {
                            decimal amount = decimal.Parse(result.SubItems[4].Text) + 1;
                            result.SubItems[4].Text = amount.ToString();
                            decimal total = amount * decimal.Parse(result.SubItems[3].Text);
                            result.SubItems[5].Text = total.ToString();
                            label11.Text = calculateTotal(listView1.Items).ToString();
                        }
                    }
                    else
                    {
                        try
                        {
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
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ไม่มีสินค้านี้");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(label11.Text == "0")
            {
                MessageBox.Show("กรุณาเพิ่มสินค้า");
            }
            else
            {
                if(text_ID_pay.Text == "") //ลูกค้าทั่วไป
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show("ยืนยันการชำระเงินใช่หรือไม่", "แจ้งเตือน", buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            int countid1 = 0;
                            int countid2 = 0;
                            decimal totalPrice1 = 0;
                            decimal totalPrice2 = 0;
                            var promotion = context.P_Promotion.First();
                            foreach (ListViewItem item in listView1.Items)
                            {
                                try
                                {
                                    if (promotion.Product_1 == item.SubItems[0].Text)
                                    {
                                        countid1 += int.Parse(item.SubItems[4].Text);
                                        totalPrice1 += decimal.Parse(item.SubItems[5].Text);
                                    }
                                    else if (promotion.Product_2 == item.SubItems[0].Text)
                                    {
                                        countid2 += int.Parse(item.SubItems[4].Text);
                                        totalPrice2 += decimal.Parse(item.SubItems[5].Text);
                                    }

                                }
                                catch (Exception)
                                {
                                }
                            }
                            string textDis = "";
                            decimal discount = 0;
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
                                textDis = "ลด 10% ทั้งหมด " + amountPack + " คู่";
                            }
                            decimal newPrice = decimal.Parse(label11.Text) - discount;
                            PayBill paybill = new PayBill(this, newPrice, textDis);
                            paybill.ShowDialog();
                            if (ChkPay)
                            {
                                try
                                {
                                    P_Order order = new P_Order();
                                    order.Cname = "ลูกค้าทั่วไป";
                                    order.OrderDate = DateTime.Now;
                                    foreach (ListViewItem item in listView1.Items)
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
                                        result2.Amount = result2.Amount - orderItem.Amount;
                                    }
                                    order.Discount = textDis;
                                    order.TotalPrice = newPrice;
                                    context.P_Order.Add(order);
                                    context.SaveChanges();
                                    listView1.Items.Clear();
                                    label11.Text = "0";
                                    text_ID_pay.Text = "";
                                    text_Name_pay.Text = "";
                                    text_Phone_pay.Text = "";
                                    ChkPay = false;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("ชำระเงินไม่สำเร็จ");
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (IdChkPay)
                        {
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult result;
                            result = MessageBox.Show("ยืนยันการชำระเงินใช่หรือไม่", "แจ้งเตือน", buttons);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                //CmChkStatus = true;
                                try
                                {
                                    int countid1 = 0;
                                    int countid2 = 0;
                                    decimal totalPrice1 = 0;
                                    decimal totalPrice2 = 0;
                                    var promotion = context.P_Promotion.First();
                                    foreach (ListViewItem item in listView1.Items)
                                    {
                                        try
                                        {
                                            if (promotion.Product_1 == item.SubItems[0].Text)
                                            {
                                                countid1 += int.Parse(item.SubItems[4].Text);
                                                totalPrice1 += decimal.Parse(item.SubItems[5].Text);
                                            }
                                            else if (promotion.Product_2 == item.SubItems[0].Text)
                                            {
                                                countid2 += int.Parse(item.SubItems[4].Text);
                                                totalPrice2 += decimal.Parse(item.SubItems[5].Text);
                                            }

                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    string textDis = "";
                                    decimal discount = 0;
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
                                        textDis = "ลด 10% ทั้งหมด " + amountPack + " คู่";
                                    }
                                    decimal newPrice = decimal.Parse(label11.Text) - discount;
                                    PayBill paybill = new PayBill(this, newPrice, textDis);
                                    paybill.ShowDialog();
                                    if (ChkPay)
                                    {
                                        try
                                        {
                                            P_Order order = new P_Order();
                                            order.Cid = int.Parse(text_ID_pay.Text);
                                            order.Cname = text_Name_pay.Text;
                                            order.Phone = text_Phone_pay.Text;
                                            order.OrderDate = DateTime.Now;
                                            foreach (ListViewItem item in listView1.Items)
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
                                                result2.Amount = result2.Amount - orderItem.Amount;
                                            }
                                            order.Discount = textDis;
                                            order.TotalPrice = newPrice;
                                            context.P_Order.Add(order);
                                            context.SaveChanges();
                                            listView1.Items.Clear();
                                            label11.Text = "0";
                                            text_ID_pay.Text = "";
                                            text_Name_pay.Text = "";
                                            text_Phone_pay.Text = "";
                                            ChkPay = false;
                                        }
                                        catch (Exception)
                                        {
                                            MessageBox.Show("ชำระเงินไม่สำเร็จ");
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("กรุณากดตรวจสอบ ID");
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("กรุณาใส่ ID เป็นตัวเลขเท่านั้น");
                    }

                }
            }
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                int amountOfpd = int.Parse(textBox14.Text);//จำนวน
                if (amountOfpd == 0)
                {
                    MessageBox.Show("สินค้าหมด");
                }
                else
                {
                    var result = listView1.FindItemWithText(textBox3.Text);
                    if (result != null)
                    {
                        int ChkAmoun = int.Parse(result.SubItems[4].Text) + 1;
                        if (ChkAmoun > amountOfpd)
                        {
                            MessageBox.Show("จำนวนสินค้าไม่เพียงพอ สินค้า ID: " + textBox3.Text + " มีจำนวน " + amountOfpd + " ชิ้น");
                        }
                        else
                        {
                            decimal amount = decimal.Parse(result.SubItems[4].Text) + 1;
                            result.SubItems[4].Text = amount.ToString();
                            decimal total = amount * decimal.Parse(result.SubItems[3].Text);
                            result.SubItems[5].Text = total.ToString();
                            label11.Text = calculateTotal(listView1.Items).ToString();
                            MessageBox.Show("เพิ่มสำเร็จ");
                        }
                    }
                    else
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
                }
            }
            catch (Exception)
            {
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(text_ID_pay.Text == "")
            {
                MessageBox.Show("กรุณากรอก ID");
            }
            else
            {
                try
                {
                    int ID = int.Parse(text_ID_pay.Text);
                    try
                    {
                        var result = context.P_Customer.Where(cm => cm.Id == ID).First();
                        text_Name_pay.Text = result.Name;
                        text_Phone_pay.Text = result.Phone;
                        IdChkPay = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ไม่พบ ID ลูกค้า");
                        text_ID_pay.Text = "";
                        text_Name_pay.Text = "";
                        text_Phone_pay.Text = "";
                        IdChkPay = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("กรุณากรอก ID เป็นตัวเลขเท่านั้น");
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            text_ID_pay.Text = "";
            text_Name_pay.Text = "";
            text_Phone_pay.Text = "";
            IdChkPay = false;
            //CmChkStatus = false;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                pCustomerBindingSource.DataSource = context.P_Customer.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("ดึงข้อมูลไม่สำเร็จ");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string tx12 = textBox12.Text;

            var result = from cm in context.P_Customer
                         where cm.Id.ToString().Contains(tx12) ||
                         cm.Name.Contains(tx12) ||
                         cm.Address.Contains(tx12) ||
                         cm.Phone.Contains(tx12)
                         select cm;
            pCustomerBindingSource.DataSource = result.ToList();
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox18.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                CmChkClick = true;
            }
            catch (Exception)
            {

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                AddnewCm addnewcm = new AddnewCm(this);
                addnewcm.ShowDialog();
                pCustomerBindingSource.DataSource = context.P_Customer.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (CmChkClick)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("ต้องการลบใช่หรือไม่", "แจ้งเตือน", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        int cm_id = int.Parse(textBox18.Text);
                        var result2 = from em in context.P_Customer
                                      where em.Id == cm_id
                                      select em;
                        context.P_Customer.Remove(result2.First());
                        context.SaveChanges();
                        MessageBox.Show("ลบสำเร็จ");
                        pCustomerBindingSource.DataSource = context.P_Customer.ToList();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ลบไม่สำเร็จ");
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกข้อมูล");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (CmChkClick)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("ยืนยันการเปลี่ยนใช่หรือไม่", "แจ้งเตือน", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        int Id = int.Parse(textBox18.Text);
                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("บันทึกสำเร็จ");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("บันทึกไม่สำเร็จ");
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ไม่เจอข้อมูล");
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกข้อมูล");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.DataSource = context.P_Product.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                var result = from pro in context.P_Product
                             where pro.Type == comboBox1.Text
                             select pro;
                dataGridView3.DataSource = result.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (textBox21.Text == "")
            {
                MessageBox.Show("กรุณากรอก ID");
            }
            else
            {
                try
                {
                    int cid = int.Parse(textBox21.Text);
                    var result = from order in context.P_Order
                                 where order.Cid == cid
                                 select order;
                    dataGridView5.DataSource = result.ToList();
                    decimal total = 0;
                    foreach (var item in result)
                    {
                        total += item.TotalPrice;
                    }
                    label27.Text = total.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("กรุณากรอก ID (เป็นตัวเลขเท่านั้น)");
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (textBox20.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อ");
            }
            else
            {
                try
                {
                    var result = from order in context.P_Order
                                 where order.P_Customer.Name == textBox20.Text
                                 select order;
                    dataGridView5.DataSource = result.ToList();
                    decimal total = 0;
                    foreach (var item in result)
                    {
                        total += item.TotalPrice;
                    }
                    label27.Text = total.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("ไม่พบข้อมูล");
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox19.Text == "")
            {
                MessageBox.Show("กรุณากรอกเบอร์โทร");
            }
            else
            {
                try
                {
                    var result = from order in context.P_Order
                                 where order.P_Customer.Phone == textBox19.Text
                                 select order;
                    dataGridView5.DataSource = result.ToList();
                    decimal total = 0;
                    foreach (var item in result)
                    {
                        total += item.TotalPrice;
                    }
                    label27.Text = total.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("ไม่พบข้อมูล");
                }
            }
        }

        private void dataGridView5_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int oid = int.Parse(dataGridView5.SelectedRows[0].Cells[0].Value.ToString());
                var result = from orderItems in context.P_OrderItem
                             where orderItems.OrderId == oid
                             select orderItems;
                dataGridView6.DataSource = result.ToList();

            }
            catch (Exception)
            {

            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                var result = from order in context.P_Order
                             where order.Cname == "ลูกค้าทั่วไป"
                             select order;
                dataGridView5.DataSource = result.ToList();
                decimal total = 0;
                foreach (var item in result)
                {
                    total += item.TotalPrice;
                }
                label27.Text = total.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            AddIdproductFromWeb addnewIdProduct = new AddIdproductFromWeb(this);
            addnewIdProduct.ShowDialog();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                var result = from pro in context.P_Product
                             where pro.Amount == 0
                             select pro;
                dataGridView3.DataSource = result.ToList();
            }
            catch (Exception)
            {
            }
        }
    }
}
