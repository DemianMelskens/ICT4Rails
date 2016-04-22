using ICT4Rails.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Rails.Forms;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;


namespace ICT4Rails
{
    public partial class InlogForm : Form
    {
        CacheData cache = new CacheData();
        public InlogForm()
        {
            InitializeComponent();
            AutoCenterContextSection();
            cache.LoadData();
        }

        //GUI Methodes
        #region GUI Methodes
        public void AutoCenterContextSection()
        {
            pl_Form_Total_Context.Location = new Point(this.ClientSize.Width / 2 - pl_Form_Total_Context.Size.Width / 2,
                                                       this.ClientSize.Height / 2 - pl_Form_Total_Context.Size.Height / 2);
            pl_Form_Total_Context.Anchor = AnchorStyles.None;
        }
        #endregion

        //Event Handlers
        #region Event Handlers

        //login event
        #region Login
        private void button1_Click(object sender, EventArgs e)
        {
            bool geenmatch = true;
            foreach (User user in cache.users)
            {
                if (user.UserName == tbUsername.Text && user.Password == tbPassword.Text)
                {
                    if (user is Admin)
                    {
                        this.Hide();
                        var AdminForm = new AdminForm(cache);
                        AdminForm.Closed += (s, args) => this.Close();
                        AdminForm.Show();
                        geenmatch = false;
                        break;
                    }
                    else if (user is Technician)
                    {
                        this.Hide();
                        var TechnicianForm = new TechnicianForm(cache);
                        TechnicianForm.Closed += (s, args) => this.Close();
                        TechnicianForm.Show();
                        geenmatch = false;
                        break;
                    }
                    else if (user is Cleaner)
                    {
                        this.Hide();
                        var CleanerForm = new CleanerForm(cache);
                        CleanerForm.Closed += (s, args) => this.Close();
                        CleanerForm.Show();
                        geenmatch = false;
                        break;
                    }
                    else if (user is Driver)
                    {
                        this.Hide();
                        var DriverForm = new DriverForm(cache);
                        DriverForm.Closed += (s, args) => this.Close();
                        DriverForm.Show();
                        geenmatch = false;
                        break;
                    }
                }
                else if (tbUsername.Text == "" || tbPassword.Text == "")
                {
                    MessageBox.Show("vul een username of wachtwoord in");
                    geenmatch = false;
                    break;
                }
            }

            if (geenmatch)
            {
                MessageBox.Show("username of wachtwoord is niet correct");
            }
        }
        #endregion

        //Recover Password Section
        #region Recover Password Section
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
            string MailAddress = tbEmailRecover.Text;
            foreach(User user in cache.users)
            {
                if(user.Email == MailAddress)
                {
                    sendMail(MailAddress, "Recover Password", user.Password);
                    pRecoverPassword.Visible = false;
                    pInlog.Visible = true;
                    break;
                }
                else
                {
                    MessageBox.Show("Wrong mail address");
                    break;
                }
            }
            // stay on recover password form if wrong
            //pRecoverPassword.Visible = false;
            //pInlog.Visible = true;
        }
        #endregion

        //Contact Admin Section
        #region Contact Admin Section
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pContactAdmin.Visible = true;

        }

        private void btnSubmitContact_Click(object sender, EventArgs e)
        {
            string Name = tbNameContact.Text;
            foreach (User user in cache.users)
            {
                if (user.UserName == Name || user.FirstName == Name)
                {
                    sendMail("ict4rails@gmail.com", Name + " Heeft contact gezocht op " + DateTime.Now.ToString(), richTextBox1.Text);
                    pRecoverPassword.Visible = false;
                    pInlog.Visible = true;
                    break;
                }
                else
                {
                    MessageBox.Show("Wrong Name");
                    break;
                }
            }
            //stay on contact form if wrong
            //pContactAdmin.Visible = false;
        }

        public void sendMail(string ontvanger, string subject, string text)
        {
            MailMessage mail = new MailMessage("ict4rails@gmail.com", ontvanger, subject, "Dit is uw wachtwoord: " + text);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 465;
            client.Credentials = new System.Net.NetworkCredential("ict4rails@gmail.com", "WELKOM12345");
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void btnCancelContact_Click(object sender, EventArgs e)
        {
            pContactAdmin.Visible = false;
        }
        #endregion

        //Other Tools
        #region Other Tools
        private void pl_Form_Total_Context_Resize(object sender, EventArgs e)
        {
            AutoCenterContextSection();
        }

        private void tbUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
        #endregion

        #endregion
    }
}