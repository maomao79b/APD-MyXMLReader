using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace ComputerDIY
{
    public partial class Goods : Form
    {
        Jack context = new Jack();
        Login login;
        public string imgLink = "KO";

        public Goods(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
        //    https://www.jib.co.th/web/product/product_list/2/43
        //    HtmlWeb web = new HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument doc = web.Load(url);
        //    var html = doc.DocumentNode.Descendants("meta");
        //    var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
        //    textBox2.Text = title.GetAttributeValue("content", "");
        //    var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
        //    textBox3.Text = description.GetAttributeValue("content", "");
        //    var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
        //    pictureBox1.Image = LoadImage(image.GetAttributeValue("content", ""));

        //    var priceblock = doc.DocumentNode.Descendants("div");
        //    var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
        //        First().InnerText;
        //    price = new string(price.Where(c => char.IsDigit(c)).ToArray());
        //    textBox4.Text = price;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            //string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
            listView1.Items.Clear();
            string url = textBox5.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            var numberOfPage = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"]//span");
            textBox6.Text = numberOfPage[1].InnerText;
            textBox8.Text = numberOfPage[0].InnerText;
            // type
            var typeOfProduct = doc.DocumentNode.SelectNodes("//span[@class=\"en\"]");
            textBox7.Text = typeOfProduct[1].InnerText;

            //textBox2.Text = titleNode.Attributes["content"].Value;

            //HtmlNode desciptionNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:description\"]");
            //textBox3.Text = desciptionNode.Attributes["content"].Value;

            //HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:image\"]");
            //pictureBox1.Image = LoadImage(imageNode.GetAttributeValue("content", ""));

            //HtmlNode priceNode = doc.DocumentNode.SelectSingleNode("//div[@class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
            //string price = priceNode.InnerText;
            //price = new string(price.Where(c => char.IsDigit(c)).ToArray());
            //textBox4.Text = price;

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            int codepage = 0;
            int count = 0;
            while (true)
            {
                string url = textBox5.Text+"/"+codepage;
                //Console.WriteLine(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                var titleNode = doc.DocumentNode.SelectNodes("//div[@class=\"cart_modal buy_promo\"]");
                if (titleNode == null)
                {
                    break;
                }
                foreach (HtmlNode node in titleNode)
                {
                    string[] data = new string[]
                    {
                        node.Attributes["data-id"].Value,
                        node.Attributes["data-name"].Value
                    };
                    listView1.Items.Add(new ListViewItem(data));
                    count++;
                }
                Console.WriteLine(count);
                codepage += 100;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //    https://www.jib.co.th/web/product/product_list/2/43
            //Console.WriteLine(listView1.SelectedItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].Text;
            // id
            string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            var html = doc.DocumentNode.Descendants("meta");
            // name
            var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
            textBox2.Text = title.GetAttributeValue("content", "");
            // detial
            var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
            textBox3.Text = description.GetAttributeValue("content", "");
            // image
            var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
            //string i = image.GetAttributeValue("content", "");
            //Console.WriteLine(i);
            pictureBox1.Image = LoadImage(image.GetAttributeValue("content", ""));
            // price
            var priceblock = doc.DocumentNode.Descendants("div");
            var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
                First().InnerText;
            price = new string(price.Where(c => char.IsDigit(c)).ToArray());
            textBox4.Text = price;
            var type = doc.DocumentNode.SelectNodes("//div[@class=\"step_nav\"]//a");
            textBox7.Text = type[2].InnerText;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codepage = 0;
            int count = 0;
            while (true)
            {
                string url = textBox5.Text + "/" + codepage;
                //Console.WriteLine(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                var titleNode = doc.DocumentNode.SelectNodes("//div[@class=\"cart_modal buy_promo\"]");
                if (titleNode == null)
                {
                    break;
                }
                //int i = 0;
                foreach (HtmlNode node in titleNode)
                {
                    P_Product product = new P_Product();
                    //string productId = node.Attributes["data-id"].Value;
                    int productId = int.Parse(node.Attributes["data-id"].Value);
                    //int productId = int.Parse(listView1.Items[i].SubItems[0].Text);
                    Console.WriteLine("Id : "+productId);
                    try
                    {
                        var checkId = context.P_Product.Where(p => p.Id == productId).First();
                        //i++;
                        continue;
                    }
                    catch (Exception)
                    {
                        string url2 = "https://www.jib.co.th/web/product/readProduct/" + productId;
                        HtmlWeb web2 = new HtmlWeb();
                        HtmlAgilityPack.HtmlDocument doc2 = web2.Load(url2);
                        var html = doc2.DocumentNode.Descendants("meta");
                        var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
                        var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
                        var priceblock = doc2.DocumentNode.Descendants("div");
                        var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
                            First().InnerText;
                        price = new string(price.Where(c => char.IsDigit(c)).ToArray());
                        var type = doc2.DocumentNode.SelectNodes("//div[@class=\"step_nav\"]//a");
                        var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
                        Random rdm = new Random();
                        //product.Id = int.Parse(productId); // Id
                        product.Id = productId; // Id
                        product.Name = title.GetAttributeValue("content", ""); // Name
                        product.Detail = description.GetAttributeValue("content", ""); // Detail
                        product.Price = decimal.Parse(price); // Price
                        product.Type = type[2].InnerText; // Type
                        product.Image = image.GetAttributeValue("content", ""); // Image
                        product.Amount = rdm.Next(0, 100); //Amount
                        context.P_Product.Add(product);
                        context.SaveChanges();
                        //i++;
                        count++;
                        
                    }
                    
                }
                codepage += 100;
            }
            //Console.WriteLine(count);
            //int result = context.SaveChanges();
            //MessageBox.Show(result+" record");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.login.Visible = true;
            //this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.login.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pProductBindingSource.DataSource = context.P_Product.ToList();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(textBox11.Text);
            var f = context.P_Product.Where(p => p.Id == id).First();
            pictureBox2.Image = LoadImage(f.Image);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddnewProduct_img newProductImg = new AddnewProduct_img(this);
            newProductImg.ShowDialog();
            try
            {
                pictureBox2.Image = LoadImage(this.imgLink);
            }
            catch (Exception)
            {
                MessageBox.Show("error link");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
