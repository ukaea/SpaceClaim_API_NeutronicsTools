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
    public partial class GeometryAssessorForm : Form
    {
        public GeometryAssessorForm()
        {
            InitializeComponent();
            //button1.Click += new EventHandler(this.button1_Click);
            textBox1.Text = Environment.CurrentDirectory + @"\GeometryAssessment.txt";
        }

        public string FileName
        {
            get { return textBox1.Text; }
        }

        public Color color1
        {
            get { return colorDialog1.Color; }
        }

        public Color color2
        {
            get { return colorDialog2.Color; }
        }

        public Color color3
        {
            get { return colorDialog3.Color; }
        }

        public Color color4
        {
            get { return colorDialog4.Color; }
        }

        public Color color5
        {
            get { return colorDialog5.Color; }
        }

        private void GeometryAssessorForm_Load(object sender, EventArgs e)
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
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                //MessageBox.Show(file);
                textBox1.Text = file;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();

            // Update the color 
            if (result == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog2.ShowDialog();

            // Update the color 
            if (result == DialogResult.OK)
            {
                button3.BackColor = colorDialog2.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog3.ShowDialog();

            // Update the color 
            if (result == DialogResult.OK)
            {
                button4.BackColor = colorDialog3.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog4.ShowDialog();

            // Update the color 
            if (result == DialogResult.OK)
            {
                button5.BackColor = colorDialog4.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog5.ShowDialog();

            // Update the color 
            if (result == DialogResult.OK)
            {
                button6.BackColor = colorDialog5.Color;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
