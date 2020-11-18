using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace All_Stars_Hotel.FORM
{
    public partial class FormSettings : Form
    {
        private string ID = "";

        private readonly string connString = "server=localhost;user id=test;database=all_stars_hotel;persistsecurityinfo=True;pwd=test";

        public FormSettings()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clear()
        {
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            tabControlUser.SelectedTab = tabPageAddUser;
        }

        private void Clear2()
        {
            textBoxUsername2.Clear();
            textBoxPassword2.Clear();
            ID = "";
        }

        private void tabPageAddUser_Leave(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridViewUser_Leave(object sender, EventArgs e)
        {
            textBoxSearchByUsername.Clear();
        }

        private void dataGridViewUser_Enter(object sender, EventArgs e)
        {
            //textBoxSearchByUsername.Clear();
        }

        private void tabPageUpdateAndDeleteUser_Leave(object sender, EventArgs e)
        {
            Clear2();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var username = textBoxUsername.Text.Trim();
            var pwd = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("Please fill out all fields.", "Required field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var cmdText = $"INSERT INTO user (username, password) VALUE ('{username}', '{pwd}')";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, conn);
                    try
                    {
                        conn.Open();
                        mySqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Added Successfully!", "User Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1062) MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else MessageBox.Show("Error! \n" + ex.ToString(), "Add User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void tabPageSearchUser_Enter(object sender, EventArgs e)
        {
            var cmdText = $"SELECT * FROM user";

            using(MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, conn);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
                dataGridViewUser.DataSource = dataTable;
            }
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewUser.Rows[e.RowIndex];
                ID = row.Cells[0].Value.ToString();
                textBoxUsername2.Text = row.Cells[1].Value.ToString();
                textBoxPassword2.Text = row.Cells[2].Value.ToString();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                var username = textBoxUsername2.Text.Trim();
                var pwd = textBoxPassword2.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
                {
                    MessageBox.Show("Please fill out all fields.", "Required field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var cmdText = $"UPDATE user SET username='{username}', password='{pwd}' WHERE id={ID}";

                    using(MySqlConnection conn = new MySqlConnection(connString))
                    {
                        MySqlCommand mySqlCommand = new MySqlCommand(cmdText, conn);
                        try
                        {
                            conn.Open();
                            mySqlCommand.ExecuteNonQuery();
                            MessageBox.Show("Updated Successfully!", "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Clear2();
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1062) MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else MessageBox.Show("Error! \n" + ex.ToString(), "Add User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
            }
            else MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                var username = textBoxUsername2.Text.Trim();
                var pwd = textBoxPassword2.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
                {
                    MessageBox.Show("Please fill out all fields.", "Required field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var cmdText = $"DELETE FROM user where id = {ID}";

                        using (MySqlConnection conn = new MySqlConnection(connString))
                        {
                            MySqlCommand mySqlCommand = new MySqlCommand(cmdText, conn);
                            try
                            {
                                conn.Open();
                                mySqlCommand.ExecuteNonQuery();
                                MessageBox.Show("Delete Successfully!", "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                Clear2();
                            }
                            catch (MySqlException ex)
                            {
                                if (ex.Number == 1062) MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                else MessageBox.Show("Error! \n" + ex.ToString(), "Add User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }
                    }
                }
            }
            else MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void textBoxSearchByUsername_TextChanged(object sender, EventArgs e)
        {
            var cmdText = $"SELECT * FROM user WHERE username LIKE '%{textBoxSearchByUsername.Text}%'";

            using(MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, conn);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
                dataGridViewUser.DataSource = dataTable;
            }
        }
    }
}
