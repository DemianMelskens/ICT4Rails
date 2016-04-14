using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Rails.Data;

namespace ICT4Rails.Forms
{
    public partial class CleanerForm : Form
    {
        private CacheData cache;
        private bool WorkScheduleOpen = false;
        private bool TramMaitenanceOpen = false;

        public CleanerForm(CacheData cache)
        {
            InitializeComponent();
            DefaultLayout();
            AutoCenterContextSection();
            this.cache = cache;
        }

        //Methodes
        #region GUI Methodes
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
            pDateSelect.Visible = false;
        }

        #endregion

        //EventsHandlers
        #region EventHandlers

        //Work Schedule Menu Items
        #region WorkScheduleMenuItems
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
                TramMaitenanceOpen = false;
                WorkScheduleOpen = true;
                btnTasks.Visible = true;
                btnTramMaitenance.Location = new Point(3, 67);
            }
        }

        private void btnTasks_Click(object sender, EventArgs e)
        {
            pDefault.Visible = false;
            pTramMaitenance.Visible = false;
            pTasks.Visible = true;
        }

        #endregion

        //Tram Maintenance Menu Items
        #region TramMaintenanceMenuItems
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
                WorkScheduleOpen = false;
                btnTramMaitenance.Location = new Point(3, 35);
                btnTrams.Visible = true;
                btnTrams.Location = new Point(3, 67);
                btnMaitenanceSchedule.Visible = true;
                btnMaitenanceSchedule.Location = new Point(3, 99);
            }
        }

        private void btnTrams_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pDefault.Visible = false;
            pTramMaitenance.Visible = true;
            lbListInfo.Text = "List Of Trams";
            dgvTrams.Visible = true;
            dgvMaitenanceSchedule.Visible = false;
            pDateSelect.Visible = false;
            pTramInfo.Visible = false;

            //load in trams from cache
            dgvTrams.Rows.Clear();
            foreach (string value in cache.trams)
            {
                string[] values = value.Split(',');
                dgvTrams.Rows.Add(values[0], values[1], values[2], values[3]);
            }
            dgvTrams.ClearSelection();
        }

        private void btnMaitenanceSchedule_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pDefault.Visible = false;
            pTramMaitenance.Visible = true;
            lbListInfo.Text = "List Of Maintenance";
            dgvMaitenanceSchedule.Rows.Clear();
            dgvMaitenanceSchedule.Rows.Add("2009", "Cleaning", "Ready for use", "12:30", " 14:30");
            dgvMaitenanceSchedule.ClearSelection();
            dgvTrams.Visible = false;
            dgvMaitenanceSchedule.Visible = true;
            pDateSelect.Visible = false;
            pTramInfo.Visible = false;
        }
        #endregion

        //Add, Delete & Other Tools Items
        #region Add, Delete & Other Tools Events

        //Maintenance Tools
        #region Maintenance Tools
        private void btnAddMaitenance_Click(object sender, EventArgs e)
        {
            pTramInfo.Visible = true;
            rtbMaitenanceDescription.Visible = true;
            lblMaitenanceDescription.Visible = true;
            cbTramStatus.Visible = true;
            lblTramStatus.Visible = true;
            lbTramInfo.Text = "Add Maintenance";
        }

        private void btnMaitenanceHistory_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pDefault.Visible = false;
            pDateSelect.Visible = true;
            lbTramInfo.Text = "Maintenance Info";
            lbListInfo.Text = "List Of Maintenance";
            pTramInfo.Visible = true;
            dgvTrams.Visible = false;
            dgvMaitenanceSchedule.Visible = true;
            dgvMaitenanceSchedule.Rows.Clear();
            dgvMaitenanceSchedule.ClearSelection();
        }

        private void dgvMaitenanceSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbTramInfo.Text = "Edit Maitenance";
            cbTramStatus.Visible = false;
            lblTramStatus.Visible = false;
            pTramInfo.Visible = true;
            lblMaitenanceDescription.Visible = true;
            rtbMaitenanceDescription.Visible = true;
        }

        private void dgvTrams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbTramInfo.Text = "Edit Tram";
            cbTramStatus.Visible = true;
            lblTramStatus.Visible = true;
            pTramInfo.Visible = true;
            pDateSelect.Visible = false;
            lblMaitenanceDescription.Visible = false;
            rtbMaitenanceDescription.Visible = false;
        }
        #endregion

        //Other Tools
        #region Other Tools
        private void CleanerForm_Resize(object sender, EventArgs e)
        {
            AutoCenterContextSection();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            var inlogform = new InlogForm();
            inlogform.Closed += (s, args) => this.Close();
            inlogform.Show();
        }
        #endregion
        #endregion
        #endregion
    }
}
