using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace ComputerDIY
{
    public partial class Admin : Form
    {
        Jack context = new Jack();
        string path_image;
        Login login;
        bool EmChkClick = false;
        bool CmChkClick = false;
        public Admin(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
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

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.login.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pEmployeeBindingSource.DataSource = context.P_Employee.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("ดึงข้อมูลไม่สำเร็จ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                this.EmChkClick = true;
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = dataGridView1.se
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.path_image = openFileDialog1.FileName;
                    //Console.WriteLine("gg="+gg);
                    pictureBox1.Image = System.Drawing.Image.FromFile(this.path_image);
                    pEmployeeBindingSource.EndEdit(); // to update binding data
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EmChkClick)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("ยืนยันการเปลี่ยนใช่หรือไม่", "แจ้งเตือน", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        int Id = int.Parse(textBox1.Text);
                        try
                        {
                            context.P_Employee.Where(em2 => em2.Username == textBox15.Text && em2.Id != Id).First();
                            MessageBox.Show("Username นี้มีการใช้งานแล้ว");
                        }
                        catch (Exception)
                        {
                            context.SaveChanges();
                            MessageBox.Show("Add Success");
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

        private void refreshData()
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string tx6 = textBox6.Text;

                var result = from em in context.P_Employee
                             where em.Id.ToString().Contains(tx6) ||
                             em.Name.Contains(tx6) ||
                             em.Address.Contains(tx6) ||
                             em.Phone.Contains(tx6) ||
                             em.Email.Contains(tx6) ||
                             em.Username.Contains(tx6) ||
                             em.Password.Contains(tx6) ||
                             em.Status.Contains(tx6) 
                             select em;
                pEmployeeBindingSource.DataSource = result.ToList();
            }
            catch (Exception)
            {
            }
            //SqlMethods.Like(a.Id, "%/" + tx6 + "/%") ||
        }


        private void btnAddEm_Click(object sender, EventArgs e)
        {
            try
            {
                AddnewEm addnewem = new AddnewEm(this);
                addnewem.ShowDialog();
                pEmployeeBindingSource.DataSource = context.P_Employee.ToList();
            }
            catch (Exception)
            {
            }
        }

        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            try
            {
                byte[] imageByteArray = null;
                FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    imageByteArray = new byte[reader.BaseStream.Length];
                    for (int i = 0; i < reader.BaseStream.Length; i++){
                        imageByteArray[i] = reader.ReadByte();
                    }
                }
                return imageByteArray;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (EmChkClick)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("ต้องการลบใช่หรือไม่", "แจ้งเตือน", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        int em_id = int.Parse(textBox1.Text);
                        var result2 = from em in context.P_Employee
                                     where em.Id == em_id
                                     select em;
                        context.P_Employee.Remove(result2.First());
                        context.SaveChanges();
                        MessageBox.Show("Remove Success");
                        pEmployeeBindingSource.DataSource = context.P_Employee.ToList();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Remove Failed");
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกข้อมูล");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox11.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                CmChkClick = true;
            }
            catch (Exception)
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
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
                        int cm_id = int.Parse(textBox11.Text);
                        var result2 = from em in context.P_Customer
                                     where em.Id == cm_id
                                     select em;
                        context.P_Customer.Remove(result2.First());
                        context.SaveChanges();
                        MessageBox.Show("Remove Success");
                        pCustomerBindingSource.DataSource = context.P_Customer.ToList();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Remove Failed");
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกข้อมูล");
            }
        }

        private void button8_Click(object sender, EventArgs e)
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
                        int Id = int.Parse(textBox11.Text);
                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Save Success");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Save Failed");
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
            //context.SaveChanges();
            //MessageBox.Show("Save Success");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string tx7 = textBox7.Text;

            var result = from cm in context.P_Customer
                         where cm.Id.ToString().Contains(tx7) ||
                         cm.Name.Contains(tx7) ||
                         cm.Address.Contains(tx7) ||
                         cm.Phone.Contains(tx7) 
                         select cm;
            pCustomerBindingSource.DataSource = result.ToList();
        }

        private void Product_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.DataSource = context.P_Product.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var result = from order in context.P_Order
                         where order.OrderDate.Year == dateTimePicker1.Value.Year &&
                         order.OrderDate.Month == dateTimePicker1.Value.Month &&
                         order.OrderDate.Day == dateTimePicker1.Value.Day
                         select order;
            dataGridView4.DataSource = result.ToList();
            decimal total=0;
            foreach(var item in result)
            {
                total += item.TotalPrice;
            }
            label12.Text = total.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown1.Value == (decimal)0)
            {
                var result = from order in context.P_Order
                             where order.OrderDate.Year == DateTime.Now.Year &&
                             order.OrderDate.Month == DateTime.Now.Month &&
                             order.OrderDate.Day == DateTime.Now.Day
                             select order;
                dataGridView4.DataSource = result.ToList();
                decimal total = 0;
                foreach (var item in result)
                {
                    total += item.TotalPrice;
                }
                label12.Text = total.ToString();
            }
            else
            {
                var result = from order in context.P_Order
                             where order.OrderDate.Year == DateTime.Now.Year &&
                             order.OrderDate.Month == DateTime.Now.Month &&
                             order.OrderDate.Day >= (DateTime.Now.Day - numericUpDown1.Value)
                             select order;
                dataGridView4.DataSource = result.ToList();
                decimal total = 0;
                foreach (var item in result)
                {
                    total += item.TotalPrice;
                }
                label12.Text = total.ToString();
            }
        }

        private void CustomerBuyReport_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            if(textBox12.Text == "")
            {
                MessageBox.Show("กรุณากรอก ID");
            }
            else
            {
                try
                {
                    int cid = int.Parse(textBox12.Text);
                    var result = from order in context.P_Order
                                 where order.Cid == cid
                                 select order;
                    dataGridView5.DataSource = result.ToList();
                    decimal total = 0;
                    foreach (var item in result)
                    {
                        total += item.TotalPrice;
                    }
                    label21.Text = total.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("กรุณากรอก ID (เป็นตัวเลขเท่านั้น)");
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อ");
            }
            else
            {
                try
                {
                    var result = from order in context.P_Order
                                 where order.P_Customer.Name == textBox13.Text
                                 select order;
                    dataGridView5.DataSource = result.ToList();
                    decimal total = 0;
                    foreach (var item in result)
                    {
                        total += item.TotalPrice;
                    }
                    label21.Text = total.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("ไม่พบข้อมูล");
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(textBox14.Text == "")
            {
                MessageBox.Show("กรุณากรอกเบอร์โทร");
            }
            else
            {
                try
                {
                    var result = from order in context.P_Order
                                 where order.P_Customer.Phone == textBox14.Text
                                 select order;
                    dataGridView5.DataSource = result.ToList();
                    decimal total = 0;
                    foreach (var item in result)
                    {
                        total += item.TotalPrice;
                    }
                    label21.Text = total.ToString();
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

        private void dataGridView5_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                string status = textBox18.Text;
                var result = context.P_Employee.Where(em => em.Status == status).ToList();
                pEmployeeBindingSource.DataSource = result.ToList();
            }
            catch (Exception)
            {

            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void Employee_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            string tx22 = textBox22.Text;

            var result = from pro in context.P_Product
                         where pro.Id.ToString().Contains(tx22)
                         select pro;
            pProductBindingSource1.DataSource = result.ToList();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string tx21 = textBox21.Text;

            var result = from pro in context.P_Product
                         where pro.Name.Contains(tx21)
                         select pro;
            pProductBindingSource1.DataSource = result.ToList();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string tx20 = textBox20.Text;

            var result = from pro in context.P_Product
                         where pro.Detail.Contains(tx20)
                         select pro;
            pProductBindingSource1.DataSource = result.ToList();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string tx19 = textBox19.Text;

            var result = from pro in context.P_Product
                         where pro.Type.Contains(tx19)
                         select pro;
            pProductBindingSource1.DataSource = result.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string tx23 = textBox23.Text;

            var result = from pro in context.P_Product
                         where pro.Id.ToString().Contains(tx23) ||
                         pro.Name.Contains(tx23) ||
                         pro.Detail.Contains(tx23) ||
                         pro.Type.Contains(tx23)
                         select pro;
            pProductBindingSource1.DataSource = result.ToList();
        }

        private void button22_Click(object sender, EventArgs e)
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
                label21.Text = total.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
        }

        //private Image LoadImage(string url)
        //{
        //    try
        //    {
        //        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        //        myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
        //        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        //        Stream streamResponse = myHttpWebResponse.GetResponseStream();
        //        Bitmap bmp = new Bitmap(streamResponse);
        //        streamResponse.Dispose();
        //        return bmp;

        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}
