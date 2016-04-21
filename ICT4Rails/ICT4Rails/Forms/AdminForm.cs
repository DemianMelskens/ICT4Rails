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
using ICT4Rails.Models;
using ICT4Rails.Models.Users;
using ICT4Rails.Forms;

namespace ICT4Rails
{
    public partial class AdminForm : Form
    {
        private CacheData cache;
        private TramQueries tramQueries = new TramQueries();
        private SegmentQueries segmentqueries = new SegmentQueries();
        private ReservationQueries reservationqueries = new ReservationQueries();

        private bool ManageAccountsOpen = false;
        private bool TramManagement = false;
        private bool TramMaitenance = false;

        private bool addreservation = true;

        private int index;
        private DialogForm dfrom;

        public AdminForm(CacheData cache)
        {
            InitializeComponent();
            StandartGUI();
            AutoCenterContextSection();
            UpdateFont();
            this.cache = cache;
            SetSegmentsVariables();
        }

        //logic Methodes
        #region Logic Methodes
        public void SetSegmentsVariables()
        {
            foreach (Segment segment in cache.segments)
            {
                TextBox textbox = new TextBox();

                foreach (var tb in pTramManagement.Controls)
                {
                    if (tb is TextBox)
                    {
                        var text = tb as TextBox;
                        if (text.Name.Equals("tb" + segment.Name))
                        {
                            segment.Textbox = text;
                            break;
                        }
                    }
                }
            }

            foreach(Segment bsegment in cache.segments)
            {
                if (bsegment.Blocked == true)
                {
                    bsegment.Textbox.BackColor = Color.Red;
                }
                else if (bsegment.Blocked == false)
                {
                    bsegment.Textbox.BackColor = Color.White;
                }
            }

            foreach(Reservation reservation in cache.reservations)
            {
                if(reservation.BeginDate.Date > DateTime.Today.Date)
                {
                    reservation.Segment.Textbox.BackColor = Color.Blue;
                }
                else if(reservation.BeginDate.Date >= DateTime.Today.Date && reservation.EndDate.Date <= DateTime.Today.Date)
                {
                    reservation.Segment.Textbox.Text = reservation.Tram.TramID;
                }
                else if (reservation.EndDate.Date < DateTime.Today.Date)
                {
                    cache.reservations.Remove(reservation);
                    reservationqueries.RemoveReservation(reservation.Segment.Name);
                }
            }
        }


        #endregion

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
            foreach (User user in cache.users)
            {
                if(user is Driver)
                {
                    dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
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
            foreach (User user in cache.users)
            {
                if (user is Technician )
                {
                    dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
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
            foreach (User user in cache.users)
            {
                if (user is Cleaner)
                {
                    dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
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

                foreach (Segment segment in cache.segments)
                {
                    if (segment.Tram != null)
                    {
                        segment.Textbox.Text = segment.Tram.TramID;
                    }
                    else
                    {
                        segment.Textbox.Text = "";
                    }
                }
            }
        }

        private void btnAddTramOverview_Click(object sender, EventArgs e)
        {
            index = 1;
            btnAddTramOverview.BackColor = Color.DimGray;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnMoveTramOverview_Click(object sender, EventArgs e)
        {
            index = 2;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.DimGray;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnDeleteTramOverview_Click(object sender, EventArgs e)
        {
            index = 3;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.DimGray;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnTramStatusOverview_Click(object sender, EventArgs e)
        {
            index = 4;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.DimGray;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnReserveSegment_Click(object sender, EventArgs e)
        {
            index = 5;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.DimGray;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnBlockSegment_Click(object sender, EventArgs e)
        {
            index = 6;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.DimGray;
            btnDeblockSegment.BackColor = Color.White;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnDeblockSegment_Click(object sender, EventArgs e)
        {
            index = 7;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.DimGray;
            btnRunSimulation.BackColor = Color.White;
        }

        private void btnRunSimulation_Click(object sender, EventArgs e)
        {
            index = 8;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;
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
            foreach (Tram tram in cache.trams)
            {
                dgvTrams.Rows.Add(tram.TramID, tram.TramType, tram.Status, tram.Length);
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
            foreach (User user in cache.users)
            {
                if(user.FirstName == Convert.ToString(row.Cells[0].Value))
                {
                    tbFirstname.Text = user.FirstName;
                    tbSurname.Text = user.SurName;
                    tbSurnamePrefix.Text = user.SurNamePrefix;
                    tbUsername.Text = user.UserName;
                    tbPassword.Text = user.Password;
                    tbEmail.Text = user.Email;
                    nudAge.Value = Convert.ToInt32(user.Age);
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

        private void schematic_Click(object sender, EventArgs e)
        {
            switch (index)
            {
                case 1:
                    bool add = false;
                    Tram addtram = null;
                    if (((TextBox)sender).Text == "")
                    {
                        dfrom = new DialogForm(((TextBox)sender).Name, cache);
                        dfrom.AddTramToOverview();
                        dfrom.ShowDialog();
                        foreach (Segment segment in cache.segments)
                        {
                            if (dfrom.Tram != null)
                            {
                                if (segment.Tram == null)
                                {

                                }
                                else if (segment.Tram.TramID == Convert.ToString(dfrom.Tram.TramID)) {
                                    MessageBox.Show("this tram is already on a segment");
                                    add = false;
                                    break;
                                }
                                else
                                {
                                    foreach (Tram tram in cache.trams)
                                    {
                                        if (dfrom.Tram.TramID == tram.TramID)
                                        {
                                            add = true;
                                            addtram = tram;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("dit segment heeft al een tram");
                    }

                    if (add)
                    {
                        ((TextBox)sender).Text = Convert.ToString(dfrom.Tram.TramID);
                        foreach (Segment segment in cache.segments)
                        {
                            if ("tb" + segment.Name == ((TextBox)sender).Name)
                            {
                                segmentqueries.ChangeSegmentTram(segment.Name, Convert.ToString(dfrom.Tram.TramID));
                                segment.Tram = addtram;
                                break;
                            }
                        }
                    }
                    else
                    {
                        add = true;
                        bool notexist = false;
                        if (dfrom.Tram != null)
                        {
                            notexist = true;
                            foreach (Tram tram in cache.trams)
                            {
                                if (dfrom.Tram.TramID == tram.TramID)
                                {
                                    notexist = false;
                                }
                            }
                        }
                        if (notexist)
                        {
                            MessageBox.Show("tram doesnt exist");
                        }

                    }
                    break;

                case 2:
                    bool move = false;
                    Tram movetram = null;
                    if (((TextBox)sender).Text == "")
                    {
                        dfrom = new DialogForm(((TextBox)sender).Name, cache);
                        dfrom.MoveTramToOverview();
                        dfrom.ShowDialog();
                        if (dfrom.Tram != null && dfrom.OldSegment != null)
                        {
                            foreach (Segment segment in cache.segments)
                            {
                                if (segment.Textbox.Text.Equals(dfrom.Tram.TramID))
                                {
                                    move = true;
                                    movetram = segment.Tram;
                                    break;
                                }
                                else
                                {
                                    move = false;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("tram is not on the overview");
                        }
                    }
                    else
                    {
                        MessageBox.Show("dit segment heeft al een tram");
                    }

                    if (move)
                    {
                        if (dfrom.Tram != null && dfrom.OldSegment != null)
                        {
                            foreach (Segment segment in cache.segments)
                            {
                                if (segment.Textbox.Name.Equals("tb" + dfrom.OldSegment.Name))
                                {
                                    segment.Textbox.Text = "";
                                    segmentqueries.ChangeSegmentTram(segment.Name, "null");
                                    segment.Tram = null;
                                    break;
                                }
                            }
                            foreach(Segment segment in cache.segments)
                            {
                                if ("tb" + segment.Name == ((TextBox)sender).Name)
                                {
                                    segment.Textbox.Text = dfrom.Tram.TramID;
                                    segmentqueries.ChangeSegmentTram(segment.Name, dfrom.Tram.TramID);
                                    segment.Tram = movetram;
                                }
                            }
                        }
                    }     
                    break;

                case 3:
                    if (((TextBox)sender).Text != "")
                    {
                        ((TextBox)sender).Text = "";
                        foreach (Segment segment in cache.segments)
                        {
                            if ("tb" + segment.Name == ((TextBox)sender).Name)
                            {
                                segmentqueries.ChangeSegmentTram(segment.Name, "null");
                                segment.Tram = null;
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("dit segment heeft geen tram");
                    }
                    break;

                case 4:
                    if (((TextBox)sender).Text != "")
                    {
                        dfrom = new DialogForm(((TextBox)sender).Name, cache);
                        dfrom.TramStatusOverview();
                        dfrom.ShowDialog();

                        foreach(Segment segment in cache.segments)
                        {
                            if(segment.Tram == null)
                            {

                            }
                            else if(segment.Tram.TramID == dfrom.Tram.TramID)
                            {
                                segment.Tram.Status = dfrom.status;
                            }
                        }

                        foreach(Tram tram in cache.trams)
                        {
                            if(tram.TramID == dfrom.Tram.TramID)
                            {
                                tram.Status = dfrom.status;
                            }
                        }
                        tramQueries.ChangeTramStatus(dfrom.statusindex, Convert.ToInt32(dfrom.Tram.TramID));
                    }
                    else
                    {
                        MessageBox.Show("dit segment heeft geen tram");
                    }
                    //add database
                    break;

                case 5:
                    if (((TextBox)sender).Text == "")
                    {
                        Segment addsegment = null;
                        Tram addtram2 = null;
                        dfrom = new DialogForm(((TextBox)sender).Name, cache);
                        dfrom.ReserveSegmentOverview();
                        dfrom.ShowDialog();

                        foreach (Segment segment in cache.segments)
                        {
                            if ("tb" + segment.Name == ((TextBox)sender).Name)
                            {
                                addsegment = segment;
                                break;
                            }
                        }

                        foreach(Tram tram in cache.trams)
                        {
                            if(tram.TramID == dfrom.Tram.TramID)
                            {
                                addtram2 = tram;
                            }
                        }

                        Reservation reservation = new Reservation(dfrom.Begindate, dfrom.Enddate, addtram2, addsegment);
                        foreach(Reservation lreservation in cache.reservations)
                        {
                            if(lreservation == reservation)
                            {
                                addreservation = false;
                            }
                        }

                        if (addreservation)
                        {
                            cache.reservations.Add(reservation);
                            reservation.Segment.Textbox.BackColor = Color.Blue;
                            reservationqueries.AddReservation(reservation.Tram.TramID, reservation.Segment.Name, reservation.BeginDate, reservation.EndDate);
                        }
                    }
                    break;

                case 6:
                    foreach (Segment segment in cache.segments)
                    {
                        if ("tb" + segment.Name == ((TextBox)sender).Name)
                        {
                            segment.Blocked = true;
                            ((TextBox)sender).BackColor = Color.Red;
                            segmentqueries.ChangeSegmentBlocked(Convert.ToInt32(segment.Name), 1);
                            break;
                        }
                    }
                    break;

                case 7:
                    foreach (Segment segment in cache.segments)
                    {
                        if ("tb" + segment.Name == ((TextBox)sender).Name)
                        {
                            segment.Blocked = false;
                            ((TextBox)sender).BackColor = Color.White;
                            segmentqueries.ChangeSegmentBlocked(Convert.ToInt32(segment.Name), 0);
                            break;
                        }
                    }
                    break;

                case 8:
                    //simulation

                    break;
            }

        }
    }
}
