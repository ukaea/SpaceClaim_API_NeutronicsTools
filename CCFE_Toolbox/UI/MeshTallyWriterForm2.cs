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
    public partial class MeshTallyWriterForm2 : Form
    {


        public MeshTallyWriterForm2()
        {
            InitializeComponent();
            //button1.Click += new EventHandler(this.button1_Click);
            string[] particles = new string[] { "p", "n" };
            comboBox1.Items.AddRange(particles);
            comboBox1.Text = "n";
            numericUpDownt.Value = 14;
            numericUpDownx.Value = 1;
            numericUpDowny.Value = 1;
            numericUpDownz.Value = 1;
        }

        public string ParticleSelection
        {
            get { return comboBox1.Text; }
        }

        public int TallyNumber
        {
            get { return (int)numericUpDownt.Value; }
        }

        public double XResolution
        {
            get { return (double)numericUpDownx.Value; }
        }

        public double YResolution
        {
            get { return (double)numericUpDowny.Value; }
        }

        public double ZResolution
        {
            get { return (double)numericUpDownz.Value; }
        }

        private void MeshTallyWriterForm2_Load(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownx_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDowny_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownz_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
