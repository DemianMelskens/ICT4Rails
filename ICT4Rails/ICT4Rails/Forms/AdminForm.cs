﻿using System;
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
using ICT4Rails.Models.Enums;
using ICT4Rails.Forms;

namespace ICT4Rails
{
    public partial class AdminForm : Form
    {
        private CacheData cache;
        private TramQueries tramQueries = new TramQueries();
        private SegmentQueries segmentqueries = new SegmentQueries();
        private UserQueries userqueries = new UserQueries();
        private ReservationQueries reservationqueries = new ReservationQueries();
        private Random _random, _tram;

        private bool ManageAccountsOpen = false;
        private bool TramManagement = false;
        private bool TramMaitenance = false;
        private bool addreservation = true;
        private bool placedTram = false;
        private bool simulation = false;
        private bool repeat = false;
        private bool maintenancehistory = false;

        private string profession;
        private string linenumber;

        private int index;
        private int timer = 0;
        private int aantalTrams;
        private int SegmentCount;
        private int trackCount;
        private int TramCount;
        private int progress = 0;
        private int number = 1;


        private Tram movetram;
        private Tram simTram;
        private DialogForm dfrom;
        private SimulationForm sform;
        private List<Tram> UntakenTrams = new List<Tram>();
        private List<Segment> UntakenSegments = new List<Segment>();

        public AdminForm(CacheData cache)
        {
            InitializeComponent();
            StandartGUI();
            AutoCenterContextSection();
            UpdateFont();
            this.cache = cache;
            SetSegmentsVariables(this.cache.segments);
            SetSegmentsVariables(this.cache.simulatiesegments);
        }

        //logic Methodes
        #region Logic Methodes
        
        public void LoadOverview()
        {
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

            foreach (Reservation reservation in cache.reservations)
            {
                if (reservation.BeginDate.Date > DateTime.Today.Date)
                {
                    reservation.Segment.Textbox.BackColor = Color.Blue;
                }
                else if (reservation.BeginDate.Date >= DateTime.Today.Date && reservation.EndDate.Date <= DateTime.Today.Date)
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

        public void SetSegmentsVariables(List<Segment> segments)
        {
            foreach (Segment segment in segments)
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
        }

        public string ConvertToLineNumber(Tramtype tramtype)
        {
            string line = "";
            if(tramtype == Tramtype.Combinos)
            {
                line = "undefined";
            }
            else if (tramtype == Tramtype.DubbelekopCombinos)
            {
                line = "undefined";
            }
            else if (tramtype == Tramtype.elevenG)
            {
                line = "5";
            }
            else if (tramtype == Tramtype.twelfG)
            {
                line = "16/24";
            }
            else if (tramtype == Tramtype.Opleidingstram)
            {
                line = "undefined";
            }

            return line;
        }

        public Tramtype ConvertToTramtype(string tramtype)
        {
            if(tramtype == "Combinos")
            {
                return Tramtype.Combinos;
            }
            else if (tramtype == "DubbelekopCombinos")
            {
                return Tramtype.DubbelekopCombinos;
            }
            else if (tramtype == "11G")
            {
                return Tramtype.elevenG;
            }
            else if (tramtype == "12G")
            {
                return Tramtype.twelfG;
            }
            else if (tramtype == "Opleidingstram")
            {
                return Tramtype.Opleidingstram;
            }

            return Tramtype.Opleidingstram;
        }

        public bool IsNextSegmentEmpty(Segment previousSegment)
        {
            bool noNextSegment = true;
            foreach (Segment segment in cache.segments)
            {
                if (Convert.ToInt32(previousSegment.Name) + 1 == Convert.ToInt32(segment.Name))
                {
                    noNextSegment = false;
                    if(segment.Textbox.Text != "")
                    {
                        if(movetram != null)
                        {
                            if (segment.Tram.TramID == movetram.TramID)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else if (segment.Blocked == true)
                    {
                        return false;
                    }
                    else if (!IsNextSegmentEmpty(segment))
                    {
                        return false;
                    }
                    else if(IsNextSegmentEmpty(segment))
                    {
                        return true;
                    }
                }
            }

            if (noNextSegment)
            {
                return true;
            }
            return false;
        }


        #endregion

        //Simulatie Methodes
        #region Simulatie Methodes
        public bool PlaceTrams(Tram tram, Track track)
        {
            track.Segments.Sort();
            Segment segment = null;
            for(int e = 0; e <= track.Segments.Count() - 1; e++)
            {
                segment = CheckIfSegmentIsFree(track.Segments.ElementAt(e), track);
                break;
            }

            if (segment != null)
            {
                if (ConvertToLineNumber(tram.TramType) == "undefined")
                {
                    if(track.LineNumber == "5" || track.LineNumber == "16/24")
                    {
                        return false;
                    }
                    else
                    {
                        if (segment.Blocked != true)
                        {
                            if (IsNextSegmentEmptySimulation(segment))
                            {
                                segment.Textbox.Text = tram.TramID;
                                UntakenSegments.Remove(segment);
                                SegmentCount--;
                                UntakenTrams.Remove(simTram);
                                TramCount--;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else if(track.LineNumber == ConvertToLineNumber(tram.TramType))
                {
                    if (segment.Blocked != true)
                    {
                        if (IsNextSegmentEmptySimulation(segment))
                        {
                            segment.Textbox.Text = tram.TramID;
                            UntakenSegments.Remove(segment);
                            SegmentCount--;
                            UntakenTrams.Remove(simTram);
                            TramCount--;
                            return true;
                        }
                        else if(!IsNextSegmentEmpty(segment))
                        {
                            var segment2 = WhatisEarliestFreeSegmentSimulation(segment);
                            if(segment2 == null)
                            {
                                return false;
                            }
                            else
                            {
                                segment2.Textbox.Text = tram.TramID;
                                UntakenSegments.Remove(segment2);
                                SegmentCount--;
                                UntakenTrams.Remove(simTram);
                                TramCount--;
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsNextSegmentEmptySimulation(Segment previousSegment)
        {
            bool noNextSegment = true;
            foreach (Segment segment in cache.simulatiesegments)
            {
                if (Convert.ToInt32(previousSegment.Name) + 1 == Convert.ToInt32(segment.Name))
                {
                    noNextSegment = false;
                    if (segment.Textbox.Text != "")
                    {
                        return false;
                    }
                    else if (segment.Blocked == true)
                    {
                        return false;
                    }
                    else if (!IsNextSegmentEmpty(segment))
                    {
                        return false;
                    }
                    else if (IsNextSegmentEmpty(segment))
                    {
                        return true;
                    }
                }
            }

            if (noNextSegment)
            {
                return true;
            }
            return false;
        }

        public Segment WhatisEarliestFreeSegmentSimulation(Segment previousSegment)
        {
            bool noNextSegment = true;
            foreach (Segment segment in cache.simulatiesegments)
            {
                if (Convert.ToInt32(previousSegment.Name) + 1 == Convert.ToInt32(segment.Name))
                {
                    noNextSegment = false;
                    if (segment.Textbox.Text != "")
                    {
                        WhatisEarliestFreeSegmentSimulation(segment);
                    }
                    else if (segment.Blocked == true)
                    {
                        foreach(Segment segment2 in cache.simulatiesegments)
                        {
                            if(Convert.ToInt32(segment.Name) + 1 == Convert.ToInt32(segment2.Name))
                            {
                                if (WhatisEarliestFreeSegmentSimulation(segment2) != null)
                                {
                                    return WhatisEarliestFreeSegmentSimulation(segment2);
                                }
                            }
                        }
                    }
                    else if (!IsNextSegmentEmpty(segment))
                    {
                        return null;
                    }
                    else if (IsNextSegmentEmpty(segment))
                    {
                        return previousSegment;
                    }
                }
            }

            if (noNextSegment)
            {
                return previousSegment;
            }
            return null;
        }

        public void ClearOverview()
        {
            foreach(Segment segment in this.cache.simulatiesegments)
            {
                segment.Textbox.Text = "";
                segment.Tram = null;
            }
        }

        public Segment CheckIfSegmentIsFree(Segment segment, Track track)
        {
            if(segment.Textbox.Text == "" && segment.Blocked != true)
            {
                return segment;
            }
            else
            {
                foreach(Segment nextsegment in track.Segments)
                {
                    if(Convert.ToInt32(nextsegment.Name) == Convert.ToInt32(segment.Name) + 1)
                    {
                        return CheckIfSegmentIsFree(nextsegment, track);
                    }
                } 
            }
            return null;
        }

        public bool IsTramOnTheOverview(Tram tram)
        {
            foreach(Segment segment in cache.segments)
            {
                if(tram.TramID == segment.Tram.TramID)
                {
                    return true;
                }
            }
            return false;
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
            lblTableText.Text = "List of Drivers";
            profession = "Bestuurder";

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
            profession = "Technicus";
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
            profession = "Schoonmaker";
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
                LoadOverview();
            }
        }

        private void btnAddTramOverview_Click(object sender, EventArgs e)
        {
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            if (simulation)
            {
                simulation = false;
                LoadOverview();
            }
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
            simulation = true;
            timer = 0;
            progress = 0;
            placedTram = false;
            repeat = false;
            btnAddTramOverview.BackColor = Color.White;
            btnMoveTramOverview.BackColor = Color.White;
            btnDeleteTramOverview.BackColor = Color.White;
            btnTramStatusOverview.BackColor = Color.White;
            btnReserveSegment.BackColor = Color.White;
            btnBlockSegment.BackColor = Color.White;
            btnDeblockSegment.BackColor = Color.White;

            dfrom = new DialogForm(null, cache);
            dfrom.SimulatieOverview();
            dfrom.ShowDialog();

            if (dfrom.DialogResult == DialogResult.OK)
            {
                sform = new SimulationForm(dfrom.AantalTrams);
                sform.Show();
                ClearOverview();
                _random = new Random();
                _tram = new Random();
                aantalTrams = dfrom.AantalTrams;
                SimulatieTimer.Start();
                trackCount = 0;
                UntakenSegments.Clear();
                UntakenTrams.Clear();

                SegmentCount = 0;
                foreach (Segment segment in cache.simulatiesegments)
                {
                    if (segment.Blocked != true)
                    {
                        UntakenSegments.Add(segment);
                        SegmentCount++;
                    }
                }

                foreach(Track track in cache.tracks)
                {
                    trackCount++;
                }

                TramCount = 0;
                foreach (Tram tram in cache.trams)
                {
                    UntakenTrams.Add(tram);
                    TramCount++;
                }
            }
            else
            {

            }
        }
        #endregion

        //Tram Maintenance Menu Items
        #region TramMaintenanceMenuEvents
        private void btnTramMaitenance_Click(object sender, EventArgs e)
        {
            maintenancehistory = false;
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
            foreach (Maintenance maintenance in cache.maintenances)
            {
                if (maintenance.Date >= DateTime.Today)
                {
                    dgvMaitenanceSchedule.Rows.Add(maintenance.Tram.TramID, maintenance.Type, maintenance.Tram.Status, maintenance.Date);
                }
            }
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

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAccountInfo.Text = "Edit Account";
            pAccountInfo.Visible = true;
            btnDelete.Visible = true;
            int rowIndex = e.RowIndex;
            if(rowIndex < 0)
            {
                return;
            }
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
            var userID = GeneralQueries.GetPrimairyKey("User", "UserID");
            var agedec = nudAge.Value;
            int age = Convert.ToInt32(agedec);

            if (profession == "Bestuurder")
            {
                User driver = new Driver(userID, tbUsername.Text,tbPassword.Text,age,tbFirstname.Text,tbSurname.Text,tbSurnamePrefix.Text,tbEmail.Text);
                cache.users.Add(driver);
                userqueries.AddUser(userID, tbUsername.Text, tbPassword.Text, age, profession, tbFirstname.Text, tbSurname.Text, tbSurnamePrefix.Text, tbEmail.Text);
            }
            if (profession == "Technicus")
            {
                User tech = new Technician(userID, tbUsername.Text, tbPassword.Text, age, tbFirstname.Text, tbSurname.Text, tbSurnamePrefix.Text, tbEmail.Text);
                cache.users.Add(tech);
                userqueries.AddUser(userID, tbUsername.Text, tbPassword.Text, age, profession, tbFirstname.Text, tbSurname.Text, tbSurnamePrefix.Text, tbEmail.Text);
            }
            if (profession == "Schoonmaker")
            {
                User clean = new Cleaner(userID, tbUsername.Text, tbPassword.Text, age, tbFirstname.Text, tbSurname.Text, tbSurnamePrefix.Text, tbEmail.Text);
                cache.users.Add(clean);
                userqueries.AddUser(userID, tbUsername.Text, tbPassword.Text, age, profession, tbFirstname.Text, tbSurname.Text, tbSurnamePrefix.Text, tbEmail.Text);
            }

            if (profession == "Bestuurder")
            {
                dgvUsers.Rows.Clear();
                foreach (User user in cache.users)
                {
                    if (user is Driver)
                    {
                        dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
                    }
                }
                dgvUsers.ClearSelection();
            }

            if (profession == "Technicus")
            {
                dgvUsers.Rows.Clear();
                foreach (User user in cache.users)
                {
                    if (user is Technician)
                    {
                        dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
                    }
                }
                dgvUsers.ClearSelection();
            }

            if (profession == "Schoonmaker")
            {
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

            tbUsername.Text = "";
            tbPassword.Text = "";
            tbFirstname.Text = "";
            tbSurname.Text = "";
            tbSurnamePrefix.Text = "";
            tbEmail.Text = "";
            nudAge.Value = 0;

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
            tbTramID.Text = "";
            tbTramType.Text = "";
            cbTramStatus.Text = "";
            tbTramLenght.Text = "";
        }

        private void btnSubmitTram_Click(object sender, EventArgs e)
        {
            Tram addtram = null;
            foreach (Tram tram in cache.trams)
            {
                if(tram.TramID == tbTramID.Text)
                {
                    addtram = tram;
                    break;
                }
                else
                {
                    addtram = null;
                }
            }

            if (addtram == null)
            {
                tramQueries.AddTram(tbTramID.Text, tbTramType.Text, dtpLastClean.Value, dtpLastReparation.Value, Convert.ToInt32(tbTramLenght.Text));
                Tram tram = new Tram(tbTramID.Text, ConvertToTramtype(tbTramType.Text), Status.GeenStatusBekent, Convert.ToInt32(tbTramLenght.Text), dtpLastClean.Value, dtpLastReparation.Value);
                cache.trams.Add(tram);
                dgvTrams.Rows.Clear();
                foreach (Tram ltram in cache.trams)
                {
                    dgvTrams.Rows.Add(ltram.TramID, ltram.TramType.ToString(), ltram.Status.ToString(), ltram.Length.ToString());
                }
                dgvTrams.ClearSelection();
            }
            else
            {
                tramQueries.ChangeTram(tbTramID.Text, tbTramType.Text, dtpLastClean.Value, dtpLastReparation.Value, Convert.ToInt32(tbTramLenght.Text));
                foreach(Tram tram in cache.trams)
                {
                    if(tbTramID.Text == tram.TramID)
                    {
                        tram.TramType = ConvertToTramtype(tbTramType.Text);
                        tram.lastClean = dtpLastClean.Value;
                        tram.lastReperation = dtpLastReparation.Value;
                        tram.Length = Convert.ToInt32(tbTramLenght.Text);
                        break;
                    }
                }
                dgvTrams.Rows.Clear();
                foreach (Tram ltram in cache.trams)
                {
                    dgvTrams.Rows.Add(ltram.TramID, ltram.TramType.ToString(), ltram.Status.ToString(), ltram.Length.ToString());
                }
                dgvTrams.ClearSelection();
            }
        }

        private void btnDeleteTram_Click(object sender, EventArgs e)
        {
            Tram deltram = null;
            foreach (Tram tram in cache.trams)
            {
                if (tram.TramID == tbTramID.Text)
                {
                    deltram = tram;
                    break;
                }
                else
                {
                    deltram = null;
                }
            }

            if(deltram != null)
            {
                tramQueries.DeleteTram(deltram.TramID);
                cache.trams.Remove(deltram);
            }

            dgvTrams.Rows.Clear();
            foreach (Tram ltram in cache.trams)
            {
                dgvTrams.Rows.Add(ltram.TramID, ltram.TramType.ToString(), ltram.Status.ToString(), ltram.Length.ToString());
            }
            dgvTrams.ClearSelection();
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
            if(rowIndex < 0)
            {
                return;
            }
            DataGridViewRow row = dgvTrams.Rows[rowIndex];
            tbTramID.Text = Convert.ToString(row.Cells[0].Value);
            tbTramType.Text = Convert.ToString(row.Cells[1].Value);
            cbTramStatus.Text = Convert.ToString(row.Cells[2].Value);
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
            if (!maintenancehistory)
            {
                lbTramInfo.Text = "Edit Maitenance";
            }
            cbTramStatus.Visible = false;
            lblTramStatus.Visible = false;
            pTramInfo.Visible = true;
            btnDeleteTram.Visible = true;
            lblMaitenanceDescription.Visible = true;
            rtbMaitenanceDescription.Visible = true;
            dtpLastClean.Visible = false;
            lblLastClean.Visible = false;
        }

        private void btnMaitenanceHistory_Click(object sender, EventArgs e)
        {
            maintenancehistory = true;
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
            foreach (Maintenance maintenance in cache.maintenances)
            {
                if (maintenance.Date < DateTime.Today)
                {
                    cbTramSelect.Items.Add(maintenance.Tram.TramID);
                }
            }

            dgvMaitenanceSchedule.Rows.Clear();
            foreach (Maintenance maintenance in cache.maintenances)
            {
                if (maintenance.Date < DateTime.Today)
                {
                    dgvMaitenanceSchedule.Rows.Add(maintenance.Tram.TramID, maintenance.Type, maintenance.Tram.Status, maintenance.Date);
                }
            }
            dgvMaitenanceSchedule.ClearSelection();

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

        #region overview Events
        private void schematic_Click(object sender, EventArgs e)
        {
            switch (index)
            {
                //Add Tram
                case 1:
                    bool add = false;
                    Tram addtram = null;
                    if (((TextBox)sender).Text == "")
                    {
                        dfrom = new DialogForm(((TextBox)sender).Name, cache);
                        dfrom.AddTramToOverview();
                        dfrom.ShowDialog();
                        if (dfrom.Tram != null)
                        {
                            foreach (Segment segment in cache.segments)
                            {
                                if ("tb" + segment.Name == ((TextBox)sender).Name)
                                {
                                    if (IsNextSegmentEmpty(segment))
                                    {
                                        if (segment.Tram == null)
                                        {
                                            add = true;
                                        }
                                        else if (segment.Tram.TramID == Convert.ToString(dfrom.Tram.TramID)) {
                                            MessageBox.Show("this tram is already on a segment");
                                            add = false;
                                        }

                                        foreach (Tram tram in cache.trams)
                                        {
                                            if (dfrom.Tram.TramID == tram.TramID)
                                            {
                                                linenumber = ConvertToLineNumber(tram.TramType);
                                                if(linenumber == "undefined")
                                                {
                                                    add = true;
                                                    addtram = tram;
                                                    break;
                                                }
                                                else if(linenumber == "5")
                                                {
                                                    if (segment.Track.LineNumber == linenumber)
                                                    {
                                                        add = true;
                                                        addtram = tram;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        add = false;
                                                        MessageBox.Show("dit type tram kan niet op dit type spoor");
                                                        break;
                                                    }
                                                }
                                                else if(linenumber == "16/24")
                                                {
                                                    if (segment.Track.LineNumber == linenumber)
                                                    {
                                                        add = true;
                                                        addtram = tram;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        add = false;
                                                        MessageBox.Show("dit type tram kan niet op dit type spoor");
                                                        break;
                                                    }
                                                }

                                            }
                                        }                                           
                                    }
                                    else
                                    {
                                        MessageBox.Show("Er staat een tram in de weg! tram kan niet geplaatst worden");
                                        break;
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

                //Move Tram
                case 2:
                    bool move = false;
                    Segment PreviousSegment = null;
                    Segment NextSegment = null;
                    if (((TextBox)sender).Text == "")
                    {
                        dfrom = new DialogForm(((TextBox)sender).Name, cache);
                        dfrom.MoveTramToOverview();
                        dfrom.ShowDialog();
                        if (dfrom.Tram != null)
                        {
                            foreach (Segment segment in cache.segments)
                            {
                                if (segment.Tram != null)
                                {
                                    if (segment.Tram.TramID == dfrom.Tram.TramID)
                                    {
                                        movetram = segment.Tram;
                                        if (IsNextSegmentEmpty(segment))
                                        {
                                            move = true;
                                            PreviousSegment = segment;
                                        }
                                        else
                                        {
                                            move = false;
                                            MessageBox.Show("Er staat een tram in de weg! tram kan niet verplaatst worden");
                                            break;
                                        }
                                    }
                                }
                                else if ("tb" + segment.Name == ((TextBox)sender).Name)
                                {
                                    if (IsNextSegmentEmpty(segment))
                                    {
                                        linenumber = ConvertToLineNumber(dfrom.Tram.TramType);
                                        if (linenumber == "undefined")
                                        {
                                            move = true;
                                            NextSegment = segment;
                                        }
                                        else if (linenumber == "5")
                                        {
                                            if (segment.Track.LineNumber == linenumber)
                                            {
                                                move = true;
                                                NextSegment = segment;
                                            }
                                            else
                                            {
                                                move = false;
                                                MessageBox.Show("dit type tram kan niet op dit type spoor");
                                                break;
                                            }
                                        }
                                        else if (linenumber == "16/24")
                                        {
                                            if (segment.Track.LineNumber == linenumber)
                                            {
                                                move = true;
                                                NextSegment = segment;
                                            }
                                            else
                                            {
                                                move = false;
                                                MessageBox.Show("dit type tram kan niet op dit type spoor");
                                                break;
                                            }
                                        }                                        
                                    }
                                    else
                                    {
                                        move = false;
                                        MessageBox.Show("Er staat een tram in de weg! tram kan niet verplaatst worden");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("dit segment heeft al een tram");
                    }

                    if (move)
                    {
                        NextSegment.Tram = PreviousSegment.Tram;
                        NextSegment.Textbox.Text = PreviousSegment.Tram.TramID;
                        segmentqueries.ChangeSegmentTram(NextSegment.Name, NextSegment.Tram.TramID);
                        PreviousSegment.Tram = null;
                        PreviousSegment.Textbox.Text = "";
                        segmentqueries.ChangeSegmentTram(PreviousSegment.Name, "null");
                    }     
                    break;

                case 3:
                    if (((TextBox)sender).Text != "")
                    {
                        foreach (Segment segment in cache.segments)
                        {
                            if ("tb" + segment.Name == ((TextBox)sender).Name)
                            {
                                if (IsNextSegmentEmpty(segment))
                                {
                                    ((TextBox)sender).Text = "";
                                    segmentqueries.ChangeSegmentTram(segment.Name, "null");
                                    segment.Tram = null;
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Er staat een tram in de weg! tram kan niet verwijderd worden");
                                    break;
                                }
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
                            else if(dfrom.Tram == null)
                            {

                            }
                            else if(segment.Tram.TramID == dfrom.Tram.TramID)
                            {
                                segment.Tram.Status = dfrom.status;
                            }
                        }

                        foreach(Tram tram in cache.trams)
                        {
                            if (tram == null)
                            {

                            }
                            else if (dfrom.Tram == null)
                            {

                            }
                            else if (tram.TramID == dfrom.Tram.TramID)
                            {
                                tram.Status = dfrom.status;
                                tramQueries.ChangeTramStatus(dfrom.statusindex, Convert.ToInt32(dfrom.Tram.TramID));
                            }
                        }    
                    }
                    else
                    {
                        MessageBox.Show("dit segment heeft geen tram");
                    }
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
                            else if(reservation.BeginDate >= lreservation.BeginDate && reservation.BeginDate <= lreservation.EndDate)
                            {
                                addreservation = false;
                            }
                            else if (reservation.EndDate >= lreservation.BeginDate && reservation.EndDate <= lreservation.EndDate)
                            {
                                addreservation = false;
                            }
                        }

                        linenumber = ConvertToLineNumber(addtram2.TramType);
                        if (linenumber == "5")
                        {
                            if (addsegment.Track.LineNumber == linenumber)
                            {
                                addreservation = true;
                            }
                            else
                            {
                                addreservation = false;
                            }
                        }
                        else if (linenumber == "16/24")
                        {
                            if (addsegment.Track.LineNumber == linenumber)
                            {
                                addreservation = true;
                            }
                            else
                            {
                                addreservation = false;
                            }
                        }

                        if (addreservation)
                        {
                            if (reservation.BeginDate.Date > DateTime.Today.Date)
                            {
                                reservation.Segment.Textbox.BackColor = Color.Blue;
                                cache.reservations.Add(reservation);
                                reservationqueries.AddReservation(reservation.Tram.TramID, reservation.Segment.Name, reservation.BeginDate, reservation.EndDate);
                            }
                            else if (reservation.BeginDate.Date >= DateTime.Today.Date && reservation.EndDate.Date <= DateTime.Today.Date)
                            {
                                reservation.Segment.Textbox.Text = reservation.Tram.TramID;
                                cache.reservations.Add(reservation);
                                reservationqueries.AddReservation(reservation.Tram.TramID, reservation.Segment.Name, reservation.BeginDate, reservation.EndDate);
                            }
                        }
                    }
                    break;

                case 6:
                    if (((TextBox)sender).Text == "")
                    {
                        foreach (Segment segment in cache.segments)
                        {
                            if ("tb" + segment.Name == ((TextBox)sender).Name)
                            {
                                if (((TextBox)sender).BackColor == Color.Blue)
                                {
                                    foreach (Reservation reservation in cache.reservations)
                                    {
                                        if (reservation.Segment.Textbox.Name == ((TextBox)sender).Name)
                                        {
                                            cache.reservations.Remove(reservation);
                                            reservationqueries.RemoveReservation(reservation.Segment.Name);
                                            break;
                                        }
                                    }
                                    segment.Blocked = true;
                                    ((TextBox)sender).BackColor = Color.Red;
                                    segmentqueries.ChangeSegmentBlocked(Convert.ToInt32(segment.Name), 1);
                                    break;
                                }
                                else
                                {
                                    segment.Blocked = true;
                                    ((TextBox)sender).BackColor = Color.Red;
                                    segmentqueries.ChangeSegmentBlocked(Convert.ToInt32(segment.Name), 1);
                                    break;
                                }
                            }
                        }      
                    }
                    else
                    {
                        MessageBox.Show("Er staat in Tram op dit Segment");
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
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            userqueries.DeleteUser(tbFirstname.Text,tbSurname.Text,tbEmail.Text);
            foreach (User user in cache.users)
            {
                if (user.FirstName == tbFirstname.Text)
                {
                    cache.users.Remove(user);
                    break;
                }
            }

            if (profession == "Bestuurder")
            {
                dgvUsers.Rows.Clear();
                foreach (User user in cache.users)
                {
                    if (user is Driver)
                    {
                        dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
                    }
                }
            }
            else if (profession == "Schoonmaker")
            {
                dgvUsers.Rows.Clear();
                foreach (User user in cache.users)
                {
                    if (user is Cleaner)
                    {
                        dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
                    }
                }
            }
            else if (profession == "Technicus")
            {
                dgvUsers.Rows.Clear();
                foreach (User user in cache.users)
                {
                    if (user is Technician)
                    {
                        dgvUsers.Rows.Add(user.FirstName, user.Age, user.UserName);
                    }
                }
            }
        }

        private void cbTramSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMaitenanceSchedule.Rows.Clear();
            foreach (Maintenance maintenance in cache.maintenances)
            {
                if (maintenance.Date < DateTime.Today)
                {
                    if (maintenance.Tram.TramID == cbTramSelect.Text)
                        dgvMaitenanceSchedule.Rows.Add(maintenance.Tram.TramID, maintenance.Type, maintenance.Tram.Status, maintenance.Date);
                }
            }
            dgvMaitenanceSchedule.ClearSelection();
        }

        private void dtpTramSelect_ValueChanged(object sender, EventArgs e)
        {
            dgvMaitenanceSchedule.Rows.Clear();
            foreach (Maintenance maintenance in cache.maintenances)
            {
                if (maintenance.Date < DateTime.Today)
                {
                    if (maintenance.Date == dtpTramSelect.Value)
                        dgvMaitenanceSchedule.Rows.Add(maintenance.Tram.TramID, maintenance.Type, maintenance.Tram.Status, maintenance.Date);
                }
            }
            dgvMaitenanceSchedule.ClearSelection();
        }

        private void SimulatieTimer_Tick(object sender, EventArgs e)
        {
            if(timer < aantalTrams)
            {
                if(!repeat)
                {
                    simTram = UntakenTrams.ElementAt(_tram.Next(TramCount));
                    timer++;
                }

                if(number >= trackCount)
                {
                    number = 0;
                }

                var simTrack = cache.tracks.ElementAt(number);
                number++;
                if (PlaceTrams(simTram, simTrack))
                {
                    placedTram = true;
                    progress++;
                    sform.SetProgress(dfrom.AantalTrams, progress);
                }
                else
                {
                    placedTram = false;
                }

                if (placedTram)
                {
                    repeat = false;
                }
                else
                {
                    repeat = true;
                }
            }
            else
            {
                SimulatieTimer.Stop();
                sform.Close();          
            }
        }
        #endregion
    }
}
