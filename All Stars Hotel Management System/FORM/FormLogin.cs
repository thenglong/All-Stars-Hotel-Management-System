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

namespace All_Stars_Hotel.FORM
{
    public partial class FormLogin : Form
    {
        private readonly string connString = "server=localhost;user id=test;database=all_stars_hotel;persistsecurityinfo=True;pwd=test";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxShowPWD_Click(object sender, EventArgs e)
        {
            pictureBoxShowPWD.Hide();
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBoxHidePWD.Show();
        }

        private void pictureBoxHidePWD_Click(object sender, EventArgs e)
        {
            pictureBoxHidePWD.Hide();
            textBoxPassword.UseSystemPasswordChar = true;
            pictureBoxShowPWD.Show();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var username = textBoxUsername.Text.Trim();
            var pwd = textBoxPassword.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("Please fill out all fields.", "Required field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                var cmdText = $"SELECT username, password FROM user WHERE username = '{username}' and password = '{pwd}'";
                // MySql Connection
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                MySqlDataReader dataReader;
                try
                {
                    // Open connection
                    conn.Open();

                    // execute find user 
                    dataReader = cmd.ExecuteReader();

                    // if user exist
                    if (dataReader.Read())
                    {
                        FormDashboard formDashboard = new FormDashboard();
                        formDashboard.Username = username;
                        formDashboard.Show();
                        //textBoxUsername.Clear();
                        textBoxPassword.Clear();
                        conn.Close();
                    }
                    else MessageBox.Show("Invalid Username or Password", "Username or Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // set the Form KeyPreview property to true first then do this and say goodbye to the beep sound
                e.Handled = true;
                buttonLogin.PerformClick();
            }
        }
    }
}
