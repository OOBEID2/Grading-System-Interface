using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if(username == "admin" && password == "123") //username: 3247 - password: pass123
            {
                this.Hide();
                Form2 open = new Form2();
                open.Show();
            }
            else
            {
                MessageBoxIcon IconType;
                MessageBoxButtons ButtonType;
                IconType = MessageBoxIcon.Warning;
                ButtonType = MessageBoxButtons.RetryCancel;
                DialogResult result = MessageBox.Show("The username or password you entered is incorrect.", "Try again!", ButtonType, IconType);
            }

            btnLogin.BackColor = Color.LightGreen;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.LightSteelBlue;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
