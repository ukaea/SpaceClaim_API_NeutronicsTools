using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCFE_Toolbox.UI
{
    public partial class Pipes_Radius : Form
    {
        public Pipes_Radius()
        {
            InitializeComponent();
            innerRadius.Enabled = false;
        }

        public bool hollow
        {
            get { return hollowCheck.Checked; }
        }

        public decimal outRadius
        {
            get { return outerRadius.Value; }
        }

        public decimal inRadius
        {
            get { return innerRadius.Value; }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void hollowCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (hollowCheck.Checked)
            {
                innerRadius.Enabled = true;
            }
            else
            {
                innerRadius.Enabled = false;
            }
        }
    }
}
