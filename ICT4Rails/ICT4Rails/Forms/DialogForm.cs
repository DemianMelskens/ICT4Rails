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
using ICT4Rails.Data;

namespace ICT4Rails.Forms
{
    public partial class DialogForm : Form
    {
        private CacheData cache;
        public Reservation reservation { get; set; }
        public Tram Tram { get; set; }
        public Segment OldSegment { get; set; }

        private string task;
        private string segmentid;

        public DialogForm(string segmentid, CacheData cache)
        {
            InitializeComponent();
            this.segmentid = segmentid;
            this.cache = cache;
            tbSegmentID.Text = segmentid;
            foreach(string value in cache.trams)
            {
                string[] values = value.Split(',');
                tbTramID.Items.Add(values[0]);
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
            pl_Form_Total_Context.Anchor = AnchorStyles.None;
        }

        public void AddTramToOverview()
        {
            pAddMoveDelete.Visible = true;
            pStatusOverview.Visible = false;
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
        }

        public void ReserveSegmentOverview()
        {
            pAddMoveDelete.Visible = true;
            pStatusOverview.Visible = false;
            dtpBeginDate.Visible = true;
            dtpEndDate.Visible = true;
            lblBeginDate.Visible = true;
            lblEndDate.Visible = true;
        }

        public void RunSimulationOverview()
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(pAddMoveDelete.Visible == true)
            {
                if(task == "Add/Delete")
                {
                    if (tbTramID.Text != "")
                    {
                        Tram = new Tram(Convert.ToInt32(tbTramID.Text));
                        DialogResult = DialogResult.OK;
                    }
                }
                else if (task == "Move")
                {
                    Tram = new Tram(Convert.ToInt32(tbTramID.Text));
                    foreach(string value in cache.segments)
                    {
                        string[] values = value.Split(',');
                        if(values[4] == tbTramID.Text)
                        {
                            OldSegment = new Segment(values[0]);
                            break;
                        }
                    }
                    DialogResult = DialogResult.OK;
                }
            }
            else if (pStatusOverview.Visible == true)
            {
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
