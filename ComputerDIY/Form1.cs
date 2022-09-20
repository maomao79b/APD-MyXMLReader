using HtmlAgilityPack;
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

namespace ComputerDIY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private Image LoadImage(string url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream streamResponse = myHttpWebResponse.GetResponseStream();
            Bitmap bmp = new Bitmap(streamResponse);
            streamResponse.Dispose();
            return bmp;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
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
            string url = textBox5.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            var numberOfPage = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"]//span");
            textBox6.Text = numberOfPage[1].InnerText;

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
            //Console.WriteLine(listView1.SelectedItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].Text;
            string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            var html = doc.DocumentNode.Descendants("meta");
            var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
            textBox2.Text = title.GetAttributeValue("content", "");
            var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
            textBox3.Text = description.GetAttributeValue("content", "");
            var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
            pictureBox1.Image = LoadImage(image.GetAttributeValue("content", ""));

            var priceblock = doc.DocumentNode.Descendants("div");
            var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
                First().InnerText;
            price = new string(price.Where(c => char.IsDigit(c)).ToArray());
            textBox4.Text = price;
        }
    }
}
