using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace greeenEats
{
    public partial class loginpage : Form
    {
        public loginpage()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }


        private void login_btn_Click(object sender, EventArgs e)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
                .Build();

            string server = configuration["Server"];
            string database = configuration["Database"];
            string username = configuration["Username"];
            string password = configuration["Password"];

            MySQLConnector connector = new MySQLConnector(server, database, username, password);

            if (connector.OpenConnection())
            {
                // Connection successful, proceed with login verification

                string enteredEmail = guna2TextBox1.Text;
                string enteredPassword = guna2TextBox2.Text;

                // Hash the entered password
                string hashedPassword = HashPassword(enteredPassword);

                string query = $"SELECT COUNT(*) FROM user WHERE email= '{enteredEmail}' AND password = '{hashedPassword}'";

                MySqlCommand command = connector.CreateCommand(query);
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    // Valid login credentials
                    MessageBox.Show("Login successful!");
                    Form1 objhf = new Form1();
                    objhf.Show();
                    this.Hide();
                }
                else
                {
                    // Invalid login credentials
                    MessageBox.Show("Invalid login credentials. Please try again.");
                }

                connector.CloseConnection();
            }
            else
            {
                // Connection failed
                MessageBox.Show("Unable to establish a connection to the database. Please try again later.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
