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
    public partial class DriverForm : Form
    {
        private bool WorkScheduleOpen = false;
        private bool TramToolsOpen = false;

        public DriverForm()
        {
            InitializeComponent();
            DefaultLayout();
        }

        private void DriverForm_Resize(object sender, EventArgs e)
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
            btnTramTools.Location = new Point(3, 35);
            btnTramStatus.Visible = false;
            btnPlaceTram.Visible = false;
        }

        private void btnWorkSchedule_Click(object sender, EventArgs e)
        {
            if (WorkScheduleOpen)
            {
                WorkScheduleOpen = false;
                pTasks.Visible = false;
                pDefault.Visible = true;
                DefaultLayout();
            }
            else
            {
                DefaultLayout();
                TramToolsOpen = false;
                WorkScheduleOpen = true;
                btnTramTools.Location = new Point(3, 67);
                btnTasks.Visible = true;
                btnTasks.Location = new Point(3, 35);
            }
        }

        private void btnTramTools_Click(object sender, EventArgs e)
        {
            if (TramToolsOpen)
            {
                TramToolsOpen = false;
                pDefault.Visible = true;
                DefaultLayout();
            }
            else
            {
                DefaultLayout();
                pTasks.Visible = false;
                TramToolsOpen = true;
                WorkScheduleOpen = false;
                btnTramTools.Location = new Point(3, 35);
                btnTramStatus.Visible = true;
                btnTramStatus.Location = new Point(3, 67);
                btnPlaceTram.Visible = true;
                btnPlaceTram.Location = new Point(3, 99);
            }
        }

        private void btnTasks_Click(object sender, EventArgs e)
        {
            pTasks.Visible = true;
            pDefault.Visible = false;
            pTramTools.Visible = false;
        }

        private void btnTramStatus_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pTramTools.Visible = true;
            pSide2.Visible = true;
            pDefault.Visible = false;
            pSide1.Visible = false;
        }

        private void btnPlaceTram_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pTramTools.Visible = true;
            pSide2.Visible = false;
            pDefault.Visible = false;
            pSide1.Visible = true;
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
