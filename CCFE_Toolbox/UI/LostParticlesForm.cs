using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CCFE_Toolbox.UI
{
    public partial class LostParticlesForm : Form
    {


        public LostParticlesForm()
        {
            InitializeComponent();
            //button1.Click += new EventHandler(this.button1_Click);
            numericUpDown1.Enabled = false;
            string[] colours = new string[] { "Blue", "Red", "Green", "Orange", "Black", "Yellow", "Salmon Pink" };
            comboBox1.Items.AddRange(colours);
            comboBox1.Text = "Green";
        }

        public string FileName
        {
            get { return textBox1.Text; }
        }

        public bool ShowVectors
        {
            get { return checkBox1.Checked; }
        }

        public double MaxParticle
        {
            get { return (double)numericUpDown1.Value; }
        }

        public bool MaxParticleBool
        {
            get { return checkBox2.Checked; }
        }

        public string ColourSelection
        {
            get { return comboBox1.Text; }
        }

        private void LostParticlesForm_Load(object sender, EventArgs e)
        {
            /*
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                MessageBox.Show(file);
            }
            */

            
            /*
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            }
            */
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                //MessageBox.Show(file);
                textBox1.Text = file;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
