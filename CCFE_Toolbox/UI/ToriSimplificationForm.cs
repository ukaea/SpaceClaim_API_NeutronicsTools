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
    public partial class tori_simp : Form
    {
        public tori_simp()
        {
            InitializeComponent();
            
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;

            standardpipeCheck.Checked = Properties.Settings.Default.UserToriStand;
            userdefinedCheck.Checked = Properties.Settings.Default.UserToriDefined;

            outer_dia.SelectedIndex=Properties.Settings.Default.UserToriStandDia;
            schedule_num.SelectedIndex=Properties.Settings.Default.UserToriSchedule;
            standardpipeCheck.Checked=Properties.Settings.Default.UserToriStand;
            numericUpDown1.Value=Properties.Settings.Default.UserToriD1;
            checkBox1.Checked=Properties.Settings.Default.UserToriD2Select;
            numericUpDown2.Value=Properties.Settings.Default.UserToriD2;
            checkBox2.Checked=Properties.Settings.Default.UserToriD3Select ;
            numericUpDown3.Value=Properties.Settings.Default.UserToriD3;
            checkBox3.Checked=Properties.Settings.Default.UserToriD4Select;
            numericUpDown3.Value=Properties.Settings.Default.UserToriD4;
            numericUpDown5.Value=Properties.Settings.Default.UserToriDeviation;
            userdefinedCheck.Checked=Properties.Settings.Default.UserToriDefined;
        }

        public decimal maximumCyl
        {
            get { return numericUpDown5.Value; }
        }

        public decimal Radius1
        {
            get { return numericUpDown1.Value; }
        }

        public decimal Radius2
        {
            get { return numericUpDown2.Value; }
        }

        public decimal Radius3
        {
            get { return numericUpDown3.Value; }
        }

        public decimal Radius4
        {
            get { return numericUpDown4.Value; }
        }

        public bool radii1
        {
            get { return userdefinedCheck.Checked; }
        }
        public bool radii2
        {
            get { return checkBox1.Checked; }
        }

        public bool radii3
        {
            get { return checkBox2.Checked; }
        }

        public bool radii4
        {
            get { return checkBox3.Checked; }
        }

        public bool fulltorus
        {
            get { return checkBox5.Checked; }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                numericUpDown3.Enabled = true;
            }
            else
            {
                numericUpDown3.Enabled = false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown2.Enabled = true;
            }
            else
            {
                numericUpDown2.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                numericUpDown4.Enabled = true;
            }
            else
            {
                numericUpDown4.Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void standardpipeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (standardpipeCheck.Checked)
            {
                outer_dia.Enabled = true;
                schedule_num.Enabled = true;
                userdefinedCheck.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
            }
            else
            {
                outer_dia.Enabled = false;
                schedule_num.Enabled = false;
                userdefinedCheck.Enabled = true;
            }
        }

        private void outer_dia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void userdefinedCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (userdefinedCheck.Checked) 
            {
                standardpipeCheck.Enabled = false;
                outer_dia.Enabled = false;
                schedule_num.Enabled = false;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
            }
            else
            {
                standardpipeCheck.Enabled = true;
                outer_dia.Enabled = false;
                schedule_num.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
            }

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
        public bool userdefined
        {
            get { return userdefinedCheck.Checked; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // set the new value of comboBoxSelection 
            Properties.Settings.Default.UserToriStandDia = outer_dia.SelectedIndex;
            Properties.Settings.Default.UserToriSchedule = schedule_num.SelectedIndex;
            Properties.Settings.Default.UserToriStand = standardpipeCheck.Checked;
            Properties.Settings.Default.UserToriD1 = numericUpDown1.Value;
            Properties.Settings.Default.UserToriD2Select = checkBox1.Checked;
            Properties.Settings.Default.UserToriD2 = numericUpDown2.Value;
            Properties.Settings.Default.UserToriD3Select = checkBox2.Checked;
            Properties.Settings.Default.UserToriD3 = numericUpDown3.Value;
            Properties.Settings.Default.UserToriD4Select = checkBox3.Checked;
            Properties.Settings.Default.UserToriD4 = numericUpDown3.Value;
            Properties.Settings.Default.UserToriDeviation = numericUpDown5.Value;
            Properties.Settings.Default.UserToriDefined = userdefinedCheck.Checked;
            
        }
    }
}
