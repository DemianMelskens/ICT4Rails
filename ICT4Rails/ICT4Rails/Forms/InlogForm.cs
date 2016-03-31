using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.Hide();
            var AdminForm = new AdminForm();
            AdminForm.Closed += (s, args) => this.Close();
            AdminForm.Show();

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
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pContactAdmin.Visible = true;

        }
    }
}
