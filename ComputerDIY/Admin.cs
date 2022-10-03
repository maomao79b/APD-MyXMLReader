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

namespace ComputerDIY
{
    public partial class Admin : Form
    {
        Jack context = new Jack();
        string path_image;
        Login login;
        public Admin(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            //this.login.Close();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.login.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pEmployeeBindingSource.DataSource = context.P_Employee.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pCustomerBindingSource.DataSource = context.P_Customer.ToList();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            //textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            //textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            //pictureBox1.Load(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = dataGridView1.se
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.path_image = openFileDialog1.FileName;
                //Console.WriteLine("gg="+gg);
                pictureBox1.Image = System.Drawing.Image.FromFile(this.path_image);
                pEmployeeBindingSource.EndEdit(); // to update binding data
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int change = context.SaveChanges();
            MessageBox.Show("Save Success");
        }

        private void refreshData()
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            string tx6 = textBox6.Text;

            var result = from em in context.P_Employee
                         where em.Id.ToString().Contains(tx6) ||
                         em.Name.Contains(tx6) ||
                         em.Address.Contains(tx6) ||
                         em.Phone.Contains(tx6) ||
                         em.Email.Contains(tx6) 
                         select em;
            pEmployeeBindingSource.DataSource = result.ToList();
            //SqlMethods.Like(a.Id, "%/" + tx6 + "/%") ||
        }


        private void btnAddEm_Click(object sender, EventArgs e)
        {
            AddnewEm addnew = new AddnewEm(this);
            addnew.ShowDialog();
            //if(addnew.ShowDialog() != DialogResult.OK)
            //{
            //    pEmployeeBindingSource.DataSource = context.P_Employee.ToList();
            //}
        }

        public static byte[] ConvertImageToByteArray(string imagePath)
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

        private void button6_Click(object sender, EventArgs e)
        {
            int em_id = int.Parse(textBox1.Text);
            var result = from em in context.P_Employee
                         where em.Id == em_id
                         select em;
            context.P_Employee.Remove(result.First());
            context.SaveChanges();
            MessageBox.Show("Remove Success");
            pEmployeeBindingSource.DataSource = context.P_Employee.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
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
