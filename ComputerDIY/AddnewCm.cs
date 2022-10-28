using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerDIY
{
    public partial class AddnewCm : Form
    {
        Jack context = new Jack();
        Admin admin;
        public AddnewCm(Admin admin)
        {
            this.admin = admin;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(textBox_id.Text);
                try
                {
                    context.P_Customer.Where(cm2 => cm2.Id == Id).First();
                    MessageBox.Show("ID นี้มีการใช้งานแล้ว");
                }
                catch (Exception)
                {
                    P_Customer customer = new P_Customer();
                    customer.Id = int.Parse(textBox_id.Text);
                    customer.Name = textBox_name.Text;
                    customer.Address = textBox_address.Text;
                    customer.Phone = textBox_phone.Text;

                    context.P_Customer.Add(customer);
                    context.SaveChanges();
                    MessageBox.Show("Add Success");
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("กรุณาใส่ ID(ตัวเลขเท่านั้น)");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
