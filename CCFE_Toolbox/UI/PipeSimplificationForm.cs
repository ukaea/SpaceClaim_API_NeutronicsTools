using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCFE_Toolbox.CCFE_Commands
{
    public partial class PipeRadiusOptions : Form
    {

        private void LoadButton_Click(object sender, EventArgs e)
        {

        }
        public PipeRadiusOptions()
        {
            InitializeComponent();
            hollowCheck.Enabled = false;
            innerRadius.Enabled = false;
            outerRadius.Enabled = false;
            outer_dia.Enabled = false;
            schedule_num.Enabled = false;
            string[] colours = new string[] { "Blue", "Red", "Green", "Orange", "Black", "Yellow", "Salmon Pink" };
            colour_dropdown.Items.AddRange(colours);
            colour_dropdown.Text = "Green";

            outer_dia.SelectedIndex = Properties.Settings.Default.UserStandDia;
            schedule_num.SelectedIndex = Properties.Settings.Default.UserSchedule;
            standardpipeCheck.Checked = Properties.Settings.Default.UserStandard;
            outerRadius.Value = Properties.Settings.Default.UserOD;
            innerRadius.Value = Properties.Settings.Default.UserID;
            userdefinedCheck.Checked = Properties.Settings.Default.UserDefined;
            hollowCheck.Checked = Properties.Settings.Default.UserHollow;
            colour_dropdown.SelectedIndex = Properties.Settings.Default.UserColour;

        }

        public string get_colour
        {
            get { return colour_dropdown.SelectedItem.ToString(); }
        }
        public bool hollow
        {
            get { return hollowCheck.Checked; }
        }
        public bool userdefined
        {
            get { return userdefinedCheck.Checked; }
        }

        public decimal outRadius
        {
            get { return outerRadius.Value; }
        }

        public decimal inRadius
        {
            get { return innerRadius.Value; }
        }

        public string get_standard_dia
        {
            get { return outer_dia.SelectedItem.ToString(); }
        }

        public string get_schedule_num
        {
            get { return schedule_num.SelectedItem.ToString(); }
        }

        public bool standardpipe
        {
            get { return standardpipeCheck.Checked; }
        }

        private void label1_Click(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void okbutton_Click(object sender, EventArgs e)
        {

            // set the new value of comboBoxSelection 
            Properties.Settings.Default.UserStandDia = outer_dia.SelectedIndex;
            Properties.Settings.Default.UserSchedule = schedule_num.SelectedIndex;
            Properties.Settings.Default.UserStandard = standardpipeCheck.Checked;
            Properties.Settings.Default.UserOD = outerRadius.Value;
            Properties.Settings.Default.UserID = innerRadius.Value;
            Properties.Settings.Default.UserDefined = userdefinedCheck.Checked;
            Properties.Settings.Default.UserHollow = hollowCheck.Checked;
            Properties.Settings.Default.UserColour = colour_dropdown.SelectedIndex;

            //apply the changes to the settings file  
            Properties.Settings.Default.Save();
        }

        private void PipeRadiusOptions_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (userdefinedCheck.Checked)
            {
                outerRadius.Enabled = true;
                hollowCheck.Enabled = true;
                outer_dia.Enabled = false;
                schedule_num.Enabled = false;
                standardpipeCheck.Checked = false;
                standardpipeCheck.Enabled = false;
            }
            else
            {
                hollowCheck.Checked = false;
                hollowCheck.Enabled = false;
                outerRadius.Enabled = false;
                standardpipeCheck.Enabled = true;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (standardpipeCheck.Checked)
            {
                outer_dia.Enabled = true;
                schedule_num.Enabled = true;
                hollowCheck.Enabled = false;
                outerRadius.Enabled = false;
                hollowCheck.Enabled = false;
                userdefinedCheck.Enabled = false;
            }
            else
            {
                outer_dia.Enabled = false;
                schedule_num.Enabled = false;
                userdefinedCheck.Enabled = true;
                userdefinedCheck.Checked = false;
                hollowCheck.Checked = false;
                userdefinedCheck.Enabled = true;

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
