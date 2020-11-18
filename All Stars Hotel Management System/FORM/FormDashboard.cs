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
    public partial class FormDashboard : Form
    {
        public string Username;
        public FormDashboard()
        {
            InitializeComponent();
            if (Username is null) Username = "Unknown User";
        }

        private void MoveSlidePanel(Control btn)
        {
            panelSlide.Top = btn.Top;
            panelSlide.Height = btn.Height;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            timer1.Start();
            labelUserName.Text = Username;
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            MoveSlidePanel(buttonDashboard);
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            MoveSlidePanel(buttonClient);
        }

        private void buttonRoom_Click(object sender, EventArgs e)
        {
            MoveSlidePanel(buttonRoom);
        }

        private void buttonReservation_Click(object sender, EventArgs e)
        {
            MoveSlidePanel(buttonReservation);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            //MoveSlidePanel(buttonSettings);
            FormSettings formSettings = new FormSettings();
            formSettings.Show();
        }

        private void FormDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No) e.Cancel = true;
        }

        private void FormDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            // set the Form KeyPreview property to true first then do this
            if (e.Control && e.KeyCode == Keys.K) buttonSettings.PerformClick();
        }

        private void labelLogout_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}
