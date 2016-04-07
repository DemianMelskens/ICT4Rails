using ICT4Rails.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Rails.Forms;
//using ICT4Rails.Data.Oracle;
//using ICT4Rails.Exceptions;

namespace ICT4Rails
{
    public partial class InlogForm : Form
    {
        public InlogForm()
        {
            InitializeComponent();
            AutoCenterContextSection();
        }

        private void pl_Form_Total_Context_Resize(object sender, EventArgs e)
        {
            AutoCenterContextSection();
        }

        //makes the content of the form center to the size
        public void AutoCenterContextSection()
        {
            pl_Form_Total_Context.Location = new Point(this.ClientSize.Width / 2 - pl_Form_Total_Context.Size.Width / 2,
                                                       this.ClientSize.Height / 2 - pl_Form_Total_Context.Size.Height / 2);
            pl_Form_Total_Context.Anchor = AnchorStyles.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == "Admin")
            {
                this.Hide();
                var AdminForm = new AdminForm();
                AdminForm.Closed += (s, args) => this.Close();
                AdminForm.Show();
            }
            else if (tbUsername.Text == "Technician")
            {
                this.Hide();
                var TechnicianForm = new TechnicianForm();
                TechnicianForm.Closed += (s, args) => this.Close();
                TechnicianForm.Show();
            }
            else if (tbUsername.Text == "Cleaner")
            {
                this.Hide();
                var CleanerForm = new CleanerForm();
                CleanerForm.Closed += (s, args) => this.Close();
                CleanerForm.Show();
            }
            else if (tbUsername.Text == "Driver")
            {
                this.Hide();
                var DriverForm = new DriverForm();
                DriverForm.Closed += (s, args) => this.Close();
                DriverForm.Show();
            }
            else if (tbUsername.Text == "")
            {
                MessageBox.Show("fill in a username");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pRecoverPassword.Visible = true;
            pInlog.Visible = false;
            pContactAdmin.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pInlog.Visible = true;
            pRecoverPassword.Visible = false;
            pContactAdmin.Visible = false;
        }

        private void btnSubmitRecover_Click(object sender, EventArgs e)
        {
            pRecoverPassword.Visible = false;
            pInlog.Visible = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pContactAdmin.Visible = true;

        }

        private void btnSubmitContact_Click(object sender, EventArgs e)
        {
            pContactAdmin.Visible = false;
        }

        private void btnCancelContact_Click(object sender, EventArgs e)
        {
            pContactAdmin.Visible = false;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                using (ICT4Rails.Data.DbConnection.Connection)
                {
                    lblTestConnection.ForeColor = Color.Green;
                    lblTestConnection.Text = DbConnection.Connection.State.ToString() + "!";
                }
            }
            catch(Exception ex)
            {
                lblTestConnection.Text = DbConnection.Connection.State.ToString();
            }
            
        }

        private void tbUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSignIn.PerformClick();
            }
        }
        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSignIn.PerformClick();
            }
        }
    }
}
