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
            btnDrivers.Hide();
            btnTechnicians.Hide();
            btnCleaningStaff.Hide();
            btnAddTram.Hide();
            btnMoveTram.Hide();
            btnDeleteTram.Hide();
            btnTramStatus.Hide();
            btnReserveSegment.Hide();
            btnBlockSegment.Hide();
            btnDeblockSegment.Hide();
            btnPlannedMaitenance.Hide();
            btnAddMaitenance.Hide();
            btnMaitenanceHistory.Hide();
        }

        public void showManageAccountsbuttons()
        {
            btnDrivers.Show();
            btnTechnicians.Show();
            btnCleaningStaff.Show();
        }

        public void showTramManagementbuttons()
        {
            btnAddTram.Show();
            btnAddTram.Location = new Point(3, 67);
            btnMoveTram.Show();
            btnMoveTram.Location = new Point(3, 99);
            btnDeleteTram.Show();
            btnDeleteTram.Location = new Point(3, 131);
            btnTramStatus.Show();
            btnTramStatus.Location = new Point(3, 163);
            btnReserveSegment.Show();
            btnReserveSegment.Location = new Point(3, 195);
            btnBlockSegment.Show();
            btnBlockSegment.Location = new Point(3, 227);
            btnDeblockSegment.Show();
            btnDeblockSegment.Location = new Point(3, 259);
        }

        public void showTramMaitenancebuttons()
        {
            btnPlannedMaitenance.Show();
            btnPlannedMaitenance.Location = new Point(3, 99);
            btnAddMaitenance.Show();
            btnAddMaitenance.Location = new Point(3, 131);
            btnMaitenanceHistory.Show();
            btnMaitenanceHistory.Location = new Point(3, 163);
        }

        private void btnManageAccounts_Click(object sender, EventArgs e)
        {
            if (ManageAccountsOpen)
            {
                ManageAccountsOpen = false;
                StandartGUI();
            }
            else
            {
                StandartGUI();
                btnDrivers.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnTechnicians.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnCleaningStaff.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
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
            pTramManagement.Visible = true;
            pAccountInfo.Visible = false;
            pDefault.Visible = false;
            if (TramManagement)
            {
                TramManagement = false;
                StandartGUI();
            }
            else
            {
                StandartGUI();
                btnAddTram.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnMoveTram.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnDeleteTram.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnTramStatus.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnReserveSegment.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnBlockSegment.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnDeblockSegment.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                ManageAccountsOpen = false;
                TramMaitenance = false;
                TramManagement = true;
                showTramManagementbuttons();
                btnTramManagement.Location = new Point(3, 35);
                btnTramMaitenance.Location = new Point(3, 291);
            }
        }

        private void btnTramMaitenance_Click(object sender, EventArgs e)
        {
            if(TramMaitenance)
            {
                TramMaitenance = false;
                StandartGUI();
            }
            else
            {
                StandartGUI();
                btnPlannedMaitenance.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnAddMaitenance.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                btnMaitenanceHistory.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
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
            pTramManagement.Visible = false;
            pDefault.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of drivers";
            dgvUsers.Rows.Clear();
            dgvUsers.Rows.Add("Rob", "23", "robeer");
        }

        private void btnTechnicians_Click(object sender, EventArgs e)
        {
            pTramManagement.Visible = false;
            pDefault.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of Technicians";
            dgvUsers.Rows.Clear();
            dgvUsers.Rows.Add("Romal", "19", "romalrio");
        }

        private void btnCleaningStaff_Click(object sender, EventArgs e)
        {
            pTramManagement.Visible = false;
            pDefault.Visible = false;
            pManageAccount.Visible = true;
            lblTableText.Text = "List of Cleaning staff";
            dgvUsers.Rows.Clear();
            dgvUsers.Rows.Add("Abdo", "24", "the o g");
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            pAccountInfo.Visible = true;
        }
    }
}
