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
    public partial class AdminForm : Form
    {
        private bool ManageAccountsOpen = false;
        private bool TramManagement = false;
        private bool TramMaitenance = false;

        public AdminForm()
        {
            InitializeComponent();
            StandartGUI();
            AutoCenterContextSection();
            UpdateFont();
        }

        private void AdminForm_Resize(object sender, EventArgs e)
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

        #region GUILogic
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

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dgvUsers.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            }
        }
        #endregion

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {

        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            pDefault.Visible = false;
            pTramManagement.Visible = false;
            pTramMaitenance.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of drivers";
            dgvUsers.Rows.Clear();
            dgvUsers.Rows.Add("Rob", "23", "robeer");
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
            dgvUsers.Rows.Add("Romal", "19", "romalrio");
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
            dgvUsers.Rows.Add("Abdo", "24", "abdojan");
            dgvUsers.ClearSelection();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            lblAccountInfo.Text = "Add Account";
            pAccountInfo.Visible = true;
            btnDelete.Visible = false;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAccountInfo.Text = "Edit Account";
            pAccountInfo.Visible = true;
            btnDelete.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pAccountInfo.Visible = false;
            pTramInfo.Visible = false;
        }

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

        private void btnTramMenu_Click(object sender, EventArgs e)
        {
            dgvTrams.Rows.Clear();
            dgvTrams.Rows.Add("2009", "Combino's", "Ready for use", "2");
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

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            var inlogform = new InlogForm();
            inlogform.Closed += (s, args) => this.Close();
            inlogform.Show();
        }
    }
}
