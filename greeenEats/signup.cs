using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace greeenEats
{
    public partial class signup : Form
    {
        public signup()
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void create_acc_btn_Click(object sender, EventArgs e)
        {
            loginpage objlog = new loginpage();
            objlog.Show();
            this.Hide();
        }

        private void create_acc_Click(object sender, EventArgs e)
        {
            starting_form obj_signup = new starting_form();
            obj_signup.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void signupAsStaff_btn_Click(object sender, EventArgs e)
        {
            staff_signup objstaffup = new staff_signup();
            objstaffup.Show();
            this.Hide();
        }
    }
}
