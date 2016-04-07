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
            pTramTools.Visible = false;
        }

        private void btnTramStatus_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pTramTools.Visible = true;
            pSide2.Visible = true;
            pSide1.Visible = false;
        }

        private void btnPlaceTram_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pTramTools.Visible = true;
            pSide2.Visible = false;
            pSide1.Visible = true;
        }
    }
}
