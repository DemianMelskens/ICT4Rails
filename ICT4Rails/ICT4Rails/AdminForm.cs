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

        #region GUIUserInput
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
                ManageAccountsOpen = false;
                TramManagement = false;
                TramMaitenance = true;
                showTramMaitenancebuttons();
                btnTramManagement.Location = new Point(3, 35);
                btnTramMaitenance.Location = new Point(3, 67);
            }
        }
        #endregion
    }
}
