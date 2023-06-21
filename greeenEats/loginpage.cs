using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


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
            string server = "sql12.freemysqlhosting.net";
            string database = "sql12627644";
            string username = "sql12627644";
            string password = "1Fy4wseAi7";

            MySQLConnector connector = new MySQLConnector(server, database, username, password);

            if (connector.OpenConnection())
            {
                // Connection successful, proceed with login verification

                string enteredUsername = guna2TextBox1.Text;
                string enteredPassword = guna2TextBox2.Text;

                // Hash the entered password
                string hashedPassword = HashPassword(enteredPassword);

                string query = $"SELECT COUNT(*) FROM users WHERE username = '{enteredUsername}' AND password = '{hashedPassword}'";

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
    }
}
