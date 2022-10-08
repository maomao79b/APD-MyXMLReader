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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ComputerDIY
{
    public partial class Shop : Form
    {
        Login login;
        Jack context = new Jack();
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

        }
    }
}
