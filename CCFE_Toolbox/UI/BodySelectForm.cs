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
    public partial class BodySelectForm : Form
    {
        public BodySelectForm()
        {
            InitializeComponent();

            numericUpDown2.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown3.Enabled = false;
        }

        public bool LocateCone
        {
            get { return checkBox5.Checked; }
        }

        public bool LocateTori
        {
            get { return checkBox3.Checked; }
        }

        public bool LocateHoles
        {
            get { return checkBox1.Checked; }
        }

        public bool LocateVolume
        {
            get { return checkBox2.Checked; }
        }

        public bool LocateFace
        {
            get { return checkBox4.Checked; }
        }

        public double minRadius
        {
            get { return (double)numericUpDown2.Value; }
        }

        public double minVolume
        {
            get { return (double)numericUpDown1.Value; }
        }

        public double minFace
        {
            get { return (double)numericUpDown3.Value; }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown2.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
            numericUpDown1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown3.Enabled = (checkBox4.CheckState == CheckState.Checked);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
