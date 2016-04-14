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

namespace ICT4Rails
{
    public partial class AdminForm : Form
    {
        private CacheData cache;
        private TramQueries tramQueries = new TramQueries();
        private bool ManageAccountsOpen = false;
        private bool TramManagement = false;
        private bool TramMaitenance = false;

        public AdminForm(CacheData cache)
        {
            InitializeComponent();
            StandartGUI();
            AutoCenterContextSection();
            UpdateFont();
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

        public void StandartGUI()
        {
            hideChildbuttons();
            btnTramManagement.Location = new Point(3, 35);
            btnTramMaitenance.Location = new Point(3, 67);
        }

        public void hideChildbuttons()
        {
            pLegenda.Hide();
            btnDrivers.Hide();
            btnTechnicians.Hide();
            btnCleaningStaff.Hide();
            btnAddTramOverview.Hide();
            btnMoveTramOverview.Hide();
            btnDeleteTramOverview.Hide();
            btnTramStatusOverview.Hide();
            btnReserveSegment.Hide();
            btnBlockSegment.Hide();
            btnDeblockSegment.Hide();
            btnRunSimulation.Hide();
            btnTramMenu.Hide();
            btnMaintenanceSchedule.Hide();
        }

        public void showManageAccountsbuttons()
        {
            btnDrivers.Show();
            btnTechnicians.Show();
            btnCleaningStaff.Show();
        }

        public void showTramManagementbuttons()
        {
            pLegenda.Show();

            btnAddTramOverview.Show();
            btnAddTramOverview.Location = new Point(3, 67);

            btnMoveTramOverview.Show();
            btnMoveTramOverview.Location = new Point(3, 99);

            btnDeleteTramOverview.Show();
            btnDeleteTramOverview.Location = new Point(3, 131);

            btnTramStatusOverview.Show();
            btnTramStatusOverview.Location = new Point(3, 163);

            btnReserveSegment.Show();
            btnReserveSegment.Location = new Point(3, 195);

            btnBlockSegment.Show();
            btnBlockSegment.Location = new Point(3, 227);

            btnDeblockSegment.Show();
            btnDeblockSegment.Location = new Point(3, 259);

            btnRunSimulation.Show();
            btnRunSimulation.Location = new Point(3, 291);
        }

        public void showTramMaitenancebuttons()
        {
            btnTramMenu.Show();
            btnTramMenu.Location = new Point(3, 99);

            btnMaintenanceSchedule.Show();
            btnMaintenanceSchedule.Location = new Point(3, 131);
        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dgvUsers.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            }
        }
        #endregion

        //EventHandlers
        #region EventHandlers

        //Manage Account menu Items
        #region ManageAccountMenuEvents
        private void btnManageAccounts_Click(object sender, EventArgs e)
        {
            pDefault.Visible = true;
            pTramManagement.Visible = false;
            pAccountInfo.Visible = false;
            pTramMaitenance.Visible = false;
            pTramInfo.Visible = false;
            if (ManageAccountsOpen)
            {
                ManageAccountsOpen = false;
                StandartGUI();
            }
            else
            {
                StandartGUI();
                TramManagement = false;
                TramMaitenance = false;
                ManageAccountsOpen = true;
                showManageAccountsbuttons();
                btnTramManagement.Location = new Point(3, 131);
                btnTramMaitenance.Location = new Point(3, 162);
            }
            
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            pDefault.Visible = false;
            pTramManagement.Visible = false;
            pTramMaitenance.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of drivers";


            dgvUsers.Rows.Clear();
            foreach (string value in cache.users)
            {
                string[] values = value.Split(',');
                if(values[4] == "Bestuurder")
                {
                    dgvUsers.Rows.Add(values[5], values[3], values[1]);
                }
               
            }
            dgvUsers.ClearSelection();
        }

        private void btnTechnicians_Click(object sender, EventArgs e)
        {
            pDefault.Visible = false;
            pTramManagement.Visible = false;
            pTramMaitenance.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of Technicians";
            dgvUsers.Rows.Clear();
            foreach (string value in cache.users)
            {
                string[] values = value.Split(',');
                if (values[4] == "Technicus")
                {
                    dgvUsers.Rows.Add(values[5], values[3], values[1]);
                }

            }
            dgvUsers.ClearSelection();
        }

        private void btnCleaningStaff_Click(object sender, EventArgs e)
        {
            pDefault.Visible = false;
            pTramManagement.Visible = false;
            pTramMaitenance.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of Cleaning staff";
            dgvUsers.Rows.Clear();
            foreach (string value in cache.users)
            {
                string[] values = value.Split(',');
                if (values[4] == "Schoonmaker")
                {
                    dgvUsers.Rows.Add(values[5], values[3], values[1]);
                }

            }
            dgvUsers.ClearSelection();
        }

        #endregion

        //Tram Management Menu Items
        #region TramManagementMenuEvents
        private void btnTramManagement_Click(object sender, EventArgs e)
        {
            pDefault.Visible = false;
            pManageAccount.Visible = false;
            pTramManagement.Visible = true;
            pAccountInfo.Visible = false;
            pTramMaitenance.Visible = false;
            pTramInfo.Visible = false;
            if (TramManagement)
            {
                TramManagement = false;
                StandartGUI();
            }
            else
            {
                StandartGUI();
                ManageAccountsOpen = false;
                TramMaitenance = false;
                TramManagement = true;
                showTramManagementbuttons();
                btnTramManagement.Location = new Point(3, 35);
                btnTramMaitenance.Location = new Point(3, 323);
            }
        }

        private void btnAddTramOverview_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnMoveTramOverview_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnDeleteTramOverview_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnTramStatusOverview_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnReserveSegment_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnBlockSegment_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnDeblockSegment_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void btnRunSimulation_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }
        #endregion

        //Tram Maintenance Menu Items
        #region TramMaintenanceMenuEvents
        private void btnTramMaitenance_Click(object sender, EventArgs e)
        {
            pDefault.Visible = true;
            pManageAccount.Visible = false;
            pTramManagement.Visible = false;
            pAccountInfo.Visible = false;
            pTramInfo.Visible = false;
            if (TramMaitenance)
            {
                TramMaitenance = false;
                StandartGUI();
            }
            else
            {
                StandartGUI();
                ManageAccountsOpen = false;
                TramManagement = false;
                TramMaitenance = true;
                showTramMaitenancebuttons();
                btnTramManagement.Location = new Point(3, 35);
                btnTramMaitenance.Location = new Point(3, 67);
            }
        }

        private void btnTramMenu_Click(object sender, EventArgs e)
        {
            dgvTrams.Rows.Clear();
            foreach (string value in cache.trams)
            {
                string[] values = value.Split(',');
                dgvTrams.Rows.Add(values[0], values[1], values[2], values[3]);
            }
            dgvTrams.ClearSelection();
            lbTramList.Text = "List of Trams";
            pDateSelect.Visible = false;
            dgvMaitenanceSchedule.Visible = false;
            cbTramStatus.Visible = true;
            lblTramStatus.Visible = true;
            dgvTrams.Visible = true;
            pDefault.Visible = false;
            pTramMaitenance.Visible = true;
            pTramInfo.Visible = false;
            btnAddTram.Visible = true;
            btnAddMaitenance.Visible = false;
            lblMaitenanceDescription.Visible = false;
            rtbMaitenanceDescription.Visible = false;
        }

        private void btnMaintenanceSchedule_Click(object sender, EventArgs e)
        {
            dgvMaitenanceSchedule.Rows.Clear();
            dgvMaitenanceSchedule.Rows.Add("2009", "Cleaning", "Ready for use", "12:30", " 14:30");
            dgvMaitenanceSchedule.ClearSelection();
            lbTramList.Text = "List of Maitenance";
            pTramInfo.Visible = false;
            pDateSelect.Visible = false;
            dgvTrams.Visible = true;
            pTramMaitenance.Visible = true;
            pDefault.Visible = false;
            dgvMaitenanceSchedule.Visible = true;
            btnAddTram.Visible = false;
            btnAddMaitenance.Visible = true;
        }
        #endregion

        //Add, Delete & Other Tools Items
        #region Add, Delete & Other Tools Events

        //User Tools
        #region User Tools
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            lblAccountInfo.Text = "Add Account";
            pAccountInfo.Visible = true;
            btnDelete.Visible = false;
            tbFirstname.Text = "";
            tbSurname.Text = "";
            tbSurnamePrefix.Text = "";
            tbUsername.Text = "";
            tbPassword.Text = "";
            tbEmail.Text = "";
            nudAge.Value = 0;
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            //Not implemented yet!
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAccountInfo.Text = "Edit Account";
            pAccountInfo.Visible = true;
            btnDelete.Visible = true;
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvUsers.Rows[rowIndex];
            foreach (string value in cache.users)
            {
                string[] values = value.Split(',');
                if(values[5] == Convert.ToString(row.Cells[0].Value))
                {
                    tbFirstname.Text = values[5];
                    tbSurname.Text = values[6];
                    tbSurnamePrefix.Text = values[7];
                    tbUsername.Text = values[1];
                    tbPassword.Text = values[2];
                    tbEmail.Text = values[8];
                    nudAge.Value = Convert.ToInt32(values[3]);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        #endregion
        //Tram Tools
        #region Tram Tools
        private void btnAddTram_Click(object sender, EventArgs e)
        {
            pTramInfo.Visible = true;
            btnDeleteTram.Visible = false;
            rtbMaitenanceDescription.Visible = false;
            lblMaitenanceDescription.Visible = false;
            cbTramStatus.Visible = true;
            lblTramStatus.Visible = true;
            lbTramInfo.Text = "Add Tram";
        }

        private void dgvTrams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbTramInfo.Text = "Edit Tram";
            cbTramStatus.Visible = true;
            lblTramStatus.Visible = true;
            pTramInfo.Visible = true;
            pDateSelect.Visible = false;
            btnDeleteTram.Visible = true;
            lblMaitenanceDescription.Visible = false;
            rtbMaitenanceDescription.Visible = false;

            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvTrams.Rows[rowIndex];
            tbTramID.Text = Convert.ToString(row.Cells[0].Value);
            tbTramType.Text = Convert.ToString(row.Cells[1].Value);
            if (Convert.ToString(row.Cells[2].Value) == "Defect")
            {
                cbTramStatus.DisplayMember = "Reperation required";
            }
            else if(Convert.ToString(row.Cells[2].Value) == "")
            {
                cbTramStatus.SelectedIndex = 1;
            }
            else if (Convert.ToString(row.Cells[2].Value) == "Dienst")
            {
                cbTramStatus.SelectedIndex = 2;
            }
            tbTramLenght.Text = Convert.ToString(row.Cells[3].Value);

        }
        #endregion
        //Maintenance Tools
        #region Maintenance Tools
        private void btnAddMaitenance_Click(object sender, EventArgs e)
        {
            pTramInfo.Visible = true;
            btnDeleteTram.Visible = false;
            rtbMaitenanceDescription.Visible = true;
            lblMaitenanceDescription.Visible = true;
            cbTramStatus.Visible = false;
            lblTramStatus.Visible = false;
            lbTramInfo.Text = "Add Maitenance";
        }

        private void dgvMaitenanceSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbTramInfo.Text = "Edit Maitenance";
            cbTramStatus.Visible = false;
            lblTramStatus.Visible = false;
            pTramInfo.Visible = true;
            btnDeleteTram.Visible = true;
            lblMaitenanceDescription.Visible = true;
            rtbMaitenanceDescription.Visible = true;
        }

        private void btnMaitenanceHistory_Click(object sender, EventArgs e)
        {
            lbTramInfo.Text = "Maitenance Info";
            cbTramStatus.Visible = false;
            dtpTramSelect.Value = DateTime.Today;
            pTramInfo.Visible = true;
            lblTramStatus.Visible = false;
            btnDeleteTram.Visible = true;
            dgvTrams.Visible = false;
            pDateSelect.Visible = true;
            dgvMaitenanceSchedule.Visible = true;
            lbTramList.Text = "List of Maitenance";
            lblMaitenanceDescription.Visible = true;
            rtbMaitenanceDescription.Visible = true;
            dgvMaitenanceSchedule.Rows.Clear();
        }
        #endregion
        //Other Tools
        #region Other Tools
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pAccountInfo.Visible = false;
            pTramInfo.Visible = false;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            var inlogform = new InlogForm();
            inlogform.Closed += (s, args) => this.Close();
            inlogform.Show();
        }

        private void AdminForm_Resize(object sender, EventArgs e)
        {
            AutoCenterContextSection();
        }
        #endregion

        #endregion

        #endregion

        private void btnSubmitTram_Click(object sender, EventArgs e)
        {

        }
    }
}
