using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace greeenEats
{
    public partial class signinform : Form
    {
        public signinform()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            loginpage objl = new loginpage();
            objl.Show();
            this.Hide();
        }

        private void create_acc_btn_Click(object sender, EventArgs e)
        {
           
        }

            private void create_acc_Click(object sender, EventArgs e)
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
                string enteredUsername = guna2TextBox1.Text;
                string phoneNumber = guna2TextBox2.Text;
                string email = guna2TextBox3.Text;
                string enteredPassword = guna2TextBox4.Text;
                string confirmedPassword = guna2TextBox5.Text;

                // Validate the input and check if passwords match
                if (string.IsNullOrWhiteSpace(enteredUsername) ||
                    string.IsNullOrWhiteSpace(phoneNumber) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(enteredPassword) ||
                    string.IsNullOrWhiteSpace(confirmedPassword))
                {
                    MessageBox.Show("Please fill in all the fields.");
                }
                else if (enteredPassword != confirmedPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                }
                else
                {
                    // Hash the password
                    string hashedPassword = HashPassword(enteredPassword);

                    // Insert the user into the database
                    string query = $"INSERT INTO users (username, phoneNumber, email, password) " +
                                   $"VALUES ('{enteredUsername}', '{phoneNumber}', '{email}', '{hashedPassword}')";

                    MySqlCommand command = connector.CreateCommand(query);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Account created successfully!");
                        loginpage objlog = new loginpage();
                        objlog.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create account. Please try again.");
                    }
                }

                connector.CloseConnection();
            }
            else
            {
                MessageBox.Show("Unable to establish a connection to the database. Please try again later.");
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            //username
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            //phoneNumber
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            //email
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            //password
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            //confirm password
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Add code here for label1_Click event
        }
    }
}
