using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICT4Rails.Forms
{
    public partial class TechnicianForm : Form
    {
        private bool WorkScheduleOpen = false;
        private bool TramMaitenanceOpen = false;

        public TechnicianForm()
        {
            InitializeComponent();
            DefaultLayout();
        }

        private void TechnicianForm_Resize(object sender, EventArgs e)
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

        private void DefaultLayout()
        {
            btnWorkSchedule.Location = new Point(3, 3);
            btnTasks.Visible = false;
            btnTramMaitenance.Location = new Point(3, 35);
            btnTrams.Visible = false;
            btnMaitenanceSchedule.Visible = false;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            var inlogform = new InlogForm();
            inlogform.Closed += (s, args) => this.Close();
            inlogform.Show();
        }

        private void btnAddMaitenance_Click(object sender, EventArgs e)
        {
            pTramInfo.Visible = true;
            rtbMaitenanceDescription.Visible = true;
            lblMaitenanceDescription.Visible = true;
            cbTramStatus.Visible = false;
            lblTramStatus.Visible = false;
            lbTramInfo.Text = "Add Maitenance";
        }

        private void btnTramMaitenance_Click(object sender, EventArgs e)
        {
            if (TramMaitenanceOpen)
            {
                TramMaitenanceOpen = false;
                DefaultLayout();
            }
            else
            {
                DefaultLayout();
                TramMaitenanceOpen = true;
                btnTramMaitenance.Location = new Point(3, 35);
                btnTrams.Visible = true;
                btnTrams.Location = new Point(3, 67);
                btnMaitenanceSchedule.Visible = true;
                btnMaitenanceSchedule.Location = new Point(3, 99);
            }
        }

        private void btnWorkSchedule_Click(object sender, EventArgs e)
        {
            if (WorkScheduleOpen)
            {
                WorkScheduleOpen = false;
                DefaultLayout();
            }
            else
            {
                DefaultLayout();
                WorkScheduleOpen = true;
                btnTasks.Visible = true;
                btnTramMaitenance.Location = new Point(3, 67);
            }
        }

        private void btnTrams_Click(object sender, EventArgs e)
        {
            pTramMaitenance.Visible = true;
            dgvTrams.Visible = true;
            dgvMaitenanceSchedule.Visible = false;
        }

        private void btnMaitenanceSchedule_Click(object sender, EventArgs e)
        {
            pTramMaitenance.Visible = true;
            dgvTrams.Visible = false;
            dgvMaitenanceSchedule.Visible = true;
        }
    }
}
