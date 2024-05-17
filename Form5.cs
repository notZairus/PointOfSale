using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randomshit
{
    public partial class Form5 : Form
    {

        string connstring = "server=127.0.0.1; port=3306; database=pos_db; username=root; pwd=QZr8408o;";

        system_cofig config = new system_cofig();

        public Form5()
        {
            InitializeComponent();
        }

        public Product product;

        public List<Order> orderlist = new List<Order>();

        private void Form5_Load(object sender, EventArgs e)
        {
            Color color = config.getColorScheme();
            Color fontColor = config.getFontColor();

            this.BackColor = color;

            guna2Button1.FillColor = color;
            guna2Button1.ForeColor = fontColor;

            guna2Button2.FillColor = color;
            guna2Button2.ForeColor = fontColor;

            guna2Button3.FillColor = color;
            guna2Button3.ForeColor = fontColor;

            guna2Button4.FillColor = color;
            guna2Button4.ForeColor = fontColor;

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = guna2Button3.Text;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = guna2Button2.Text;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = guna2Button1.Text;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = guna2Button4.Text;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            int holder = int.Parse(guna2TextBox1.Text) + 1;
            guna2TextBox1.Text = holder.ToString();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            int holder = int.Parse(guna2TextBox1.Text) - 1;
            guna2TextBox1.Text = holder.ToString();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            int ItemCount;

            if (product.productStock >= int.Parse(guna2TextBox1.Text))
            {
                Order temporder = new Order(product, int.Parse(guna2TextBox1.Text));

                ItemCount = int.Parse(guna2TextBox1.Text);

                orderlist.Add(temporder);

                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE product_tbl SET Stocks = Stocks - @Ewan WHERE PID = @PID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Ewan", ItemCount);
                        cmd.Parameters.AddWithValue("@PID", product.productId);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                guna2TextBox1.Text = "1";

                Hide();
            }
            else
            {
                MessageBox.Show("Insufficient Stock.");
            }
        }
    }
}
