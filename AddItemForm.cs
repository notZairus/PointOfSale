using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randomshit
{
    public partial class AddItemForm : Form
    {
        public AddItemForm()
        {
            InitializeComponent();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Byte[] ImageData;

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                ImageData = ms.ToArray();
            }

            string connstring = "server=127.0.0.1; port=3306; database=pos_db; username=root; pwd=QZr8408o;";

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query = "INSERT INTO pos_db.product_tbl (ProductImg, ProductName, Presyo, Stocks, Category) VALUES (@ProductImg, @ProductName, @Presyo, @Stocks, @Category);";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@ProductImg", MySqlDbType.LongBlob).Value = ImageData;
                    cmd.Parameters.AddWithValue("@ProductName", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Presyo", double.Parse(guna2TextBox4.Text));
                    cmd.Parameters.AddWithValue("@Stocks", int.Parse(guna2TextBox1.Text));
                    cmd.Parameters.AddWithValue("@Category", comboBox1.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                }

                conn.Close();

                MessageBox.Show("Successfully Added!");
            }

            createButton.Visible = false; createButton.Enabled = false;
            comboBox1.Enabled = true; comboBox1.Visible = true;
            label3.Enabled = true; label4.Visible = true;


            this.Dispose();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] imgdata = File.ReadAllBytes(ofd.FileName);

                MemoryStream ms = new MemoryStream(imgdata);
                
                pictureBox1.Image = Image.FromStream(ms);
            }
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            if (label4.Text == "ITEM ID")
            {
                return;
            }
            else
            {
                string connstring = "server=127.0.0.1; port=3306; database=pos_db; username=root; pwd=QZr8408o;";

                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    string query = "SELECT * FROM product_tbl WHERE PID = @PID;";
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PID", int.Parse(label4.Text));

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                guna2TextBox2.Text = reader["ProductName"].ToString();
                                guna2TextBox4.Text = reader["Presyo"].ToString();
                                guna2TextBox1.Text = reader["Stocks"].ToString();
                                comboBox1.SelectedText = reader["Category"].ToString();
                                pictureBox1.Image = toImage((byte[])reader["ProductImg"]);
                            }
                        }
                    }
                }
            }
        }

        public Image toImage(byte[] ImageData)
        {
            Image img;
            using (MemoryStream MemoryStream = new MemoryStream(ImageData))
            {
                img = Image.FromStream(MemoryStream);
            }
            return img;
        }
    }
}
