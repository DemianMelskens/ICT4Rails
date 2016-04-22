using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;
using ICT4Rails.Models.Enums;
using ICT4Rails.Data;

namespace ICT4Rails.Forms
{
    public partial class DialogForm : Form
    {
        private CacheData cache;
        public Reservation reservation { get; private set; }
        public Tram Tram { get; private set; }
        public Status status { get; private set; }
        public int statusindex { get; set; }
        public int AantalTrams { get; set; }
        public DateTime Begindate {get;set;}
        public DateTime Enddate { get; set; }

        private string task;
        private string segmentid;
        private int aantal;

        public DialogForm(string segmentid, CacheData cache)
        {
            InitializeComponent();
            this.segmentid = segmentid;
            this.cache = cache;
            tbSegmentID.Text = segmentid;
            foreach(Segment segment in cache.segments)
            {
                if("tb" + segment.Name == segmentid)
                {
                    if (segment.Tram != null)
                    {
                        tbTramStatusID.Text = segment.Tram.TramID;
                        cbTramStatus.Text = segment.Tram.Status.ToString();
                    }
                    break;
                }
            }

            aantal = 0;
            foreach (Segment segment in cache.segments)
            {
                if (segment.Blocked != true)
                {
                    aantal++;
                    cbAantalTrams.Items.Add(aantal);
                }
            }

            foreach (Tram tram in cache.trams)
            {
                tbTramID.Items.Add(tram.TramID);
            }
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
            pl_Form_Total_Context.Anchor = AnchorStyles.None; //
        }

        public void AddTramToOverview()
        {
            pAddMoveDelete.Visible = true;
            pStatusOverview.Visible = false;
            pSimulatie.Visible = false;
            task = "Add/Delete";
            dtpBeginDate.Visible = false;
            dtpEndDate.Visible = false;
            lblBeginDate.Visible = false;
            lblEndDate.Visible = false;
        }

        public void MoveTramToOverview()
        {
            pAddMoveDelete.Visible = true;
            pStatusOverview.Visible = false;
            pSimulatie.Visible = false;
            task = "Move";
            dtpBeginDate.Visible = false;
            dtpEndDate.Visible = false;
            lblBeginDate.Visible = false;
            lblEndDate.Visible = false;
        }

        public void DeleteTramToOverview()
        {
            pAddMoveDelete.Visible = true;
            pStatusOverview.Visible = false;
            pSimulatie.Visible = false;
            task = "Add/Delete";
            dtpBeginDate.Visible = false;
            dtpEndDate.Visible = false;
            lblBeginDate.Visible = false;
            lblEndDate.Visible = false;
        }

        public void TramStatusOverview()
        {
            pAddMoveDelete.Visible = false;
            pStatusOverview.Visible = true;
            pSimulatie.Visible = false;
        }

        public void ReserveSegmentOverview()
        {
            task = "Reserve"; 
            pAddMoveDelete.Visible = true;
            pStatusOverview.Visible = false;
            pSimulatie.Visible = false;
            dtpBeginDate.Visible = true;
            dtpEndDate.Visible = true;
            lblBeginDate.Visible = true;
            lblEndDate.Visible = true;
        }

        public void SimulatieOverview()
        {
            pAddMoveDelete.Visible = false;
            pStatusOverview.Visible = false;
            pSimulatie.Visible = true;
            dtpBeginDate.Visible = false;
            dtpEndDate.Visible = false;
            lblBeginDate.Visible = false;
            lblEndDate.Visible = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (pAddMoveDelete.Visible == true)
            {
                if (task == "Add/Delete")
                {
                    if (tbTramID.Text != "")
                    {
                        foreach (Tram tram in cache.trams)
                        {
                            if (tbTramID.Text == tram.TramID)
                            {
                                Tram = tram;
                                break;
                            }
                        }
                        DialogResult = DialogResult.OK;
                    }
                }
                else if (task == "Move")
                {
                    if (tbTramID.Text != "")
                    {
                        foreach (Tram tram in cache.trams)
                        {
                            if (tbTramID.Text == tram.TramID)
                            {
                                Tram = tram;
                                break;
                            }
                        }
                        DialogResult = DialogResult.OK;
                    }
                }
                else if (task == "Reserve")
                {
                    if (tbTramID.Text != "")
                    {
                        if (dtpBeginDate.Value > DateTime.Today.Date && dtpBeginDate.Value <= dtpEndDate.Value)
                        {
                            Tram = new Tram(tbTramID.Text);
                            Begindate = dtpBeginDate.Value;
                            Enddate = dtpEndDate.Value;
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Je kunt geen reserveringen maken voor in het verleden");
                        }
                    }
                }

            }
            else if (pStatusOverview.Visible == true)
            {
                statusindex = cbTramStatus.SelectedIndex + 1;
                switch (cbTramStatus.SelectedIndex + 1)
                {
                    case 1:
                        status = Status.ReadyForUse;
                        break;

                    case 2:
                        status = Status.NeedsCleaning;
                        break;

                    case 3:
                        status = Status.NeedsReperation;
                        break;

                    case 4:
                        status = Status.InRemise;
                        break;

                    case 5:
                        status = Status.Defect;
                        break;

                    case 6:
                        status = Status.GeenStatusBekent;
                        break;
                }

                if (cbTramStatus.Text == "")
                {
                    status = Status.GeenStatusBekent;
                }

                Tram = new Tram(tbTramStatusID.Text);
                DialogResult = DialogResult.OK;
            }
            else if (pSimulatie.Visible == true)
            {
                AantalTrams = Convert.ToInt32(cbAantalTrams.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
