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
using ICT4Rails.Models.Enums;
using ICT4Rails.Models;

namespace ICT4Rails.Forms
{
    public partial class DriverForm : Form
    {
        private CacheData cache;
        private TramQueries tramqueries = new TramQueries();
        private bool WorkScheduleOpen = false;
        private bool TramToolsOpen = false;
        private int rowIndex;

        public DriverForm(CacheData cache)
        {
            InitializeComponent();
            DefaultLayout();
            AutoCenterContextSection();
            this.cache = cache;
        }

        //GUI Methodes
        #region GUI Methodes
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
        #endregion

        //Event Handlers
        #region Event Handlers

        //Work Schedule Menu Items
        #region Work Schedule Menu Items
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


        private void btnTasks_Click(object sender, EventArgs e)
        {
            pTasks.Visible = true;
            pDefault.Visible = false;
            pTramTools.Visible = false;
        }
        #endregion

        //Tram Tools Menu Items
        #region Tram Tools Menu Items
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

        private void btnTramStatus_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pTramTools.Visible = true;
            pSide2.Visible = true;
            pDefault.Visible = false;
            pSide1.Visible = false;
            foreach(string value in cache.trams)
            {
                string[] values = value.Split(',');
                dgvTrams.Rows.Add(values[0], values[1], values[2], values[3]);
            }
        }

        private void btnPlaceTram_Click(object sender, EventArgs e)
        {
            pTasks.Visible = false;
            pTramTools.Visible = true;
            pSide2.Visible = false;
            pDefault.Visible = false;
            pSide1.Visible = true;
        }
        #endregion

        //Other Tools
        #region Other Tools
        private void DriverForm_Resize(object sender, EventArgs e)
        {
            AutoCenterContextSection();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            var inlogform = new InlogForm();
            inlogform.Closed += (s, args) => this.Close();
            inlogform.Show();
        }
        #endregion

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow dataRow = dgvTrams.Rows[rowIndex];
            dataRow.Cells[0].Value = tbTramID.Text;
            dataRow.Cells[1].Value = tbTramType.Text;
            dataRow.Cells[2].Value = cbTramStatus.Text;
            dataRow.Cells[3].Value = tbTramLenght.Text;

            string value = tbTramID.Text + "," + tbTramType.Text + "," + cbTramStatus.Text + "," + tbTramLenght.Text;
            cache.trams.RemoveAt(rowIndex);
            cache.trams.Insert(rowIndex, value);
            tramqueries.ChangeTramStatus(cbTramStatus.SelectedIndex + 1, Convert.ToInt32(tbTramID.Text));
            
        }

        private void dgvTrams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            DataGridViewRow row = dgvTrams.Rows[rowIndex];
            tbTramID.Text = Convert.ToString(row.Cells[0].Value);
            tbTramType.Text = Convert.ToString(row.Cells[1].Value);
            tbTramLenght.Text = Convert.ToString(row.Cells[3].Value);
        }
    }
}
