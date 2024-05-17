using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace randomshit
{
    public partial class Form2 : Form
    {
        string connstring = "server=127.0.0.1; port=3306; database=pos_db; uid=root; pwd=QZr8408o;";

        system_cofig config = new system_cofig();

        public Form2()
        {
            InitializeComponent();
        }

        int age;

        private void guna2Button3_Click(object sender, EventArgs e)
        {
             OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                guna2CirclePictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox3.Text == "" || guna2TextBox4.Text == "")
            {
                MessageBox.Show("Please fill out the necessary information");
                return;
            }

            if (age < 18)
            {
                MessageBox.Show("Im sorry, you are too young to work.");
                return;
            }
     
            if (guna2TextBox5.Text.Length < 7 || guna2TextBox6.Text.Length < 7)
            {
                MessageBox.Show("Password must not be less than 8 characters");
                return;
            }

            Image img = guna2CirclePictureBox1.Image;
            byte[] imageData;

            using (MemoryStream memstream = new MemoryStream())
            {
                img.Save(memstream, img.RawFormat);
                imageData = memstream.ToArray();
            }

            string sqlQuerry = "INSERT INTO user_tbl (FirstName, MiddleName, LastName, Gender, Age, Birthday, userName, passWord, Administrator, ImageData, Deactivated) VALUES (@FirstName, @MiddleName, @LastName, @Gender, @Age, @Birthday, @userName, @passWord, @Administrator, @ImageData, 0);";
            
            if (guna2TextBox5.Text != guna2TextBox6.Text)
            {
                MessageBox.Show("Password confirmation does not match the entered password. Please check and try once more.");
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn))
                    {
                        string gender;
                        bool admin = false;

                        if (checkBox2.Checked)
                        {
                            gender = "Female";
                        }
                        else
                        {
                            gender = "Male";
                        }

                        cmd.Parameters.AddWithValue("@FirstName", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@MiddleName", guna2TextBox2.Text);
                        cmd.Parameters.AddWithValue("@LastName", guna2TextBox3.Text);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Birthday", guna2DateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@userName", guna2TextBox4.Text);
                        cmd.Parameters.AddWithValue("@passWord", guna2TextBox5.Text);
                        cmd.Parameters.AddWithValue("@Administrator", admin);
                        cmd.Parameters.Add("@ImageData", MySqlDbType.LongBlob).Value = imageData;

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();

                    MessageBox.Show("SUCCESS!");
                    this.Close();
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                checkBox2.Checked = true;
            }

            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                checkBox1.Checked = true;
            }

            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Color color = config.getColorScheme();
            Color fontColor = config.getFontColor();

            this.BackColor = color;
            this.ForeColor = fontColor;

            guna2RadioButton4.BackColor = color;
            guna2RadioButton4.ForeColor = fontColor;

            guna2Button3.FillColor = color;
            guna2Button3.ForeColor = fontColor;

            guna2Button1.FillColor = color;
            guna2Button1.ForeColor = fontColor;

            guna2CircleButton1.ForeColor = fontColor;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            age = DateTime.Today.Year - guna2DateTimePicker1.Value.Year;
            if (guna2DateTimePicker1.Value.Month > DateTime.Today.Month)
            {
                age--;
            }
            else if (guna2DateTimePicker1.Value.Month == DateTime.Today.Month)
            {
                if (guna2DateTimePicker1.Value.Day > DateTime.Today.Day)
                {
                    age--;
                }
            }

        }
    }
}
