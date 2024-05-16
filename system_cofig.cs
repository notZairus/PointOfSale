using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomshit
{
    public class system_cofig
    {
        public Color getColorScheme()
        {
            string connstring = "server=127.0.0.1; port=3306; database=pos_db; uid=root; pwd=QZr8408o;";
            string argbColor = "";

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT ColorScheme FROM system_config", conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        argbColor = reader.GetString("ColorScheme");
                    }
                    reader.Close();
                }

                conn.Close();
            }

            ColorConverter converter = new ColorConverter();
            return (Color)converter.ConvertFromString(argbColor);

        }

        public Color getFontColor()
        {
            string connstring = "server=127.0.0.1; port=3306; database=pos_db; uid=root; pwd=QZr8408o;";
            string argbColor = "";

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT FontColor FROM system_config", conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        argbColor = reader.GetString("FontColor");
                    }
                    reader.Close();
                }

                conn.Close();
            }

            ColorConverter converter = new ColorConverter();
            return (Color)converter.ConvertFromString(argbColor);

        }

        public String getStoreName()
        {
            string connstring = "server=127.0.0.1; port=3306; database=pos_db; uid=root; pwd=QZr8408o;";
            string storeName = "";

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT StoreName FROM system_config", conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        storeName = reader.GetString("StoreName");
                    }
                    reader.Close();
                }

                conn.Close();
            }

            return storeName;
        } 
    }
}
