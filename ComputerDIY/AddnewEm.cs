using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerDIY
{
    public partial class AddnewEm : Form
    {
        Jack context = new Jack();
        Admin admin;
        string path_image;
        public AddnewEm(Admin admin)
        {
            this.admin = admin;
            InitializeComponent();
        }

        private void AddnewEm_Load(object sender, EventArgs e)
        {
            //kkk
        }

        private void btnChangeImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.path_image = openFileDialog1.FileName;
                pictureBox.Image = System.Drawing.Image.FromFile(this.path_image);
                //pEmployeeBindingSource.EndEdit(); // to update binding data
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(textBox_id.Text); 
                try
                {
                    context.P_Employee.Where(em2 => em2.Id == Id).First();
                    MessageBox.Show("ID นี้มีการใช้งานแล้ว");
                }
                catch (Exception)
                {
                    try
                    {
                        context.P_Employee.Where(em2 => em2.Username == textBox2.Text).First();
                        MessageBox.Show("Username นี้มีการใช้งานแล้ว");
                    }
                    catch (Exception)
                    {
                        try
                        {
                            context.P_Employee.Where(em2 => em2.Email == textBox_Email.Text).First();
                            MessageBox.Show("Email นี้มีการใช้งานแล้ว");
                        }
                        catch (Exception)
                        {
                            P_Employee em = new P_Employee();
                            em.Id = Id;
                            em.Name = textBox_Name.Text;
                            em.Address = textBox_Address.Text;
                            em.Phone = textBox_Phone.Text;
                            em.Email = textBox_Email.Text;
                            em.Username = textBox2.Text;
                            em.Password = textBox1.Text;
                            em.Status = textBox3.Text;
                            if (pictureBox.Image == null)
                            {
                                em.Image = null;
                            }
                            else
                            {
                                em.Image = ConvertImageToByteArray(this.path_image);
                            }
                            context.P_Employee.Add(em);
                            context.SaveChanges();
                            MessageBox.Show("เพิ่มสำเร็จ");
                            this.Close();
                        }
                    }

                }

            }
            catch (Exception)
            {
                MessageBox.Show("กรุณาใส่ ID(ตัวเลขเท่านั้น)");
            }
        }

        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                imageByteArray = new byte[reader.BaseStream.Length];
                for (int i = 0; i < reader.BaseStream.Length; i++)
                {
                    imageByteArray[i] = reader.ReadByte();
                }
            }
            return imageByteArray;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddnewEm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
