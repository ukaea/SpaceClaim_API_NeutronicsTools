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
    public partial class VoidGeneratorForm : Form
    {


        public VoidGeneratorForm()
        {
            InitializeComponent();
            //button1.Click += new EventHandler(this.button1_Click);
            numericUpDown1.Value = 5;
            numericUpDown2.Value = 30;


        }

        public int Iterations
        {
            get { return (int)numericUpDown1.Value; }
        }

        public int MaxFaces
        {
            get { return (int)numericUpDown2.Value; }
        }

        private void VoidGeneratorForm_Load(object sender, EventArgs e)
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
