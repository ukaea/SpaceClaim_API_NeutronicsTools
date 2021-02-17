using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CCFE_Toolbox.UI
{
    public partial class MeshTallyCheckerForm : Form
    {
        public MeshTallyCheckerForm()
        {
            InitializeComponent();
            checkBox1.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Checked = true;
        }


        public bool Cut
        {
            get { return checkBox1.Checked; }
        }

        public bool Plane
        {
            get { return checkBox2.Checked; }
        }


        private void MeshTallyCheckerForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = (checkBox2.CheckState == CheckState.Checked);
            if (checkBox2.CheckState == CheckState.Unchecked)
            {
                checkBox1.Checked = false;
            }
        }

    }
}
