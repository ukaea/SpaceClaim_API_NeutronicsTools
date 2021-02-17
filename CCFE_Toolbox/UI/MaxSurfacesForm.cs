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
    public partial class MaxSurfacesForm : Form
    {
        public MaxSurfacesForm()
        {
            InitializeComponent();
            string[] colours = new string[] { "Blue", "Red", "Green", "Orange", "Black", "Yellow", "Salmon Pink" };
            comboBox1.Items.AddRange(colours);
            comboBox1.Text = "Red";
            comboBox1.Enabled = false;
        }

        public string ColourSelection
        {
            get { return comboBox1.Text; }
        }

        public bool ChangeColour
        {
            get { return checkBox1.Checked; }
        }

        public double MaxSurfaces
        {
            get { return (double)numericUpDown1.Value; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }
    }
}
