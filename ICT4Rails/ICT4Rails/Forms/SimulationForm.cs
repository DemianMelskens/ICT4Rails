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
    public partial class SimulationForm : Form
    {
        public SimulationForm(int maxvalue)
        {
            InitializeComponent();
            lbInfo.Text = "0 van de " + Convert.ToString(maxvalue) + " Gedaan";
        }

        public void AutoCenterContextSection()
        {
            pl_Form_Total_Context.Location = new Point(this.ClientSize.Width / 2 - pl_Form_Total_Context.Size.Width / 2,
                                                       this.ClientSize.Height / 2 - pl_Form_Total_Context.Size.Height / 2);
            pl_Form_Total_Context.Anchor = AnchorStyles.None;
        }

        private void SimulationForm_Resize(object sender, EventArgs e)
        {
            AutoCenterContextSection();
        }

        public void SetProgress(double maxvalue, double progress)
        {
            double value = maxvalue / 100;
            double result = progress / value;
            pbProgress.Value = Convert.ToInt32(result);
            lbInfo.Text = Convert.ToInt32(progress).ToString() + " van de " + Convert.ToString(Convert.ToInt32(maxvalue)) + " Gedaan";
        }
    }
}
