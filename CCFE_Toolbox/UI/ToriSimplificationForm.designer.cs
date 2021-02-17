namespace CCFE_Toolbox.CCFE_Commands
{
    partial class tori_simp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.schedule_num = new System.Windows.Forms.ComboBox();
            this.outer_dia = new System.Windows.Forms.ComboBox();
            this.standardpipeCheck = new System.Windows.Forms.CheckBox();
            this.userdefinedCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 5;
            this.numericUpDown1.Location = new System.Drawing.Point(8, 200);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 5;
            this.numericUpDown2.Location = new System.Drawing.Point(152, 200);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(197, 305);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.DecimalPlaces = 3;
            this.numericUpDown5.Location = new System.Drawing.Point(152, 17);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown5.TabIndex = 9;
            this.numericUpDown5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label5.Location = new System.Drawing.Point(5, 15);
            this.label5.MaximumSize = new System.Drawing.Size(140, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Maximun Angular Deviation (degrees)";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(152, 177);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(122, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Second Radius (cm)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(8, 249);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(109, 17);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "Third Radius (cm)";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(152, 249);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(115, 17);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "Fourth Radius (cm)";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 58);
            this.label6.MaximumSize = new System.Drawing.Size(250, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(232, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "By default the toriodal minor radius will be used. ";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.DecimalPlaces = 5;
            this.numericUpDown4.Location = new System.Drawing.Point(152, 270);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown4.TabIndex = 3;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 5;
            this.numericUpDown3.Location = new System.Drawing.Point(8, 270);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 2;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(8, 309);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(157, 17);
            this.checkBox5.TabIndex = 16;
            this.checkBox5.Text = "Full torus (i.e. ignore planes)";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Schedule Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "DN Number";
            // 
            // schedule_num
            // 
            this.schedule_num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schedule_num.FormattingEnabled = true;
            this.schedule_num.Items.AddRange(new object[] {
            "5S",
            "10S",
            "40S",
            "80S"});
            this.schedule_num.Location = new System.Drawing.Point(152, 130);
            this.schedule_num.Name = "schedule_num";
            this.schedule_num.Size = new System.Drawing.Size(119, 21);
            this.schedule_num.TabIndex = 18;
            // 
            // outer_dia
            // 
            this.outer_dia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.outer_dia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.outer_dia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outer_dia.FormattingEnabled = true;
            this.outer_dia.Items.AddRange(new object[] {
            "6",
            "8",
            "10",
            "15",
            "20",
            "25",
            "32",
            "40",
            "50",
            "65",
            "80",
            "90",
            "100",
            "125",
            "150",
            "200",
            "250",
            "300",
            "350",
            "400",
            "450",
            "500",
            "550",
            "600",
            "750"});
            this.outer_dia.Location = new System.Drawing.Point(13, 130);
            this.outer_dia.Name = "outer_dia";
            this.outer_dia.Size = new System.Drawing.Size(117, 21);
            this.outer_dia.TabIndex = 17;
            this.outer_dia.SelectedIndexChanged += new System.EventHandler(this.outer_dia_SelectedIndexChanged);
            // 
            // standardpipeCheck
            // 
            this.standardpipeCheck.AutoSize = true;
            this.standardpipeCheck.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.standardpipeCheck.Location = new System.Drawing.Point(8, 92);
            this.standardpipeCheck.Name = "standardpipeCheck";
            this.standardpipeCheck.Size = new System.Drawing.Size(93, 17);
            this.standardpipeCheck.TabIndex = 21;
            this.standardpipeCheck.Text = "Standard Pipe";
            this.standardpipeCheck.UseVisualStyleBackColor = true;
            this.standardpipeCheck.CheckedChanged += new System.EventHandler(this.standardpipeCheck_CheckedChanged);
            // 
            // userdefinedCheck
            // 
            this.userdefinedCheck.AutoSize = true;
            this.userdefinedCheck.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.userdefinedCheck.Location = new System.Drawing.Point(7, 157);
            this.userdefinedCheck.Name = "userdefinedCheck";
            this.userdefinedCheck.Size = new System.Drawing.Size(108, 17);
            this.userdefinedCheck.TabIndex = 22;
            this.userdefinedCheck.Text = "User defined radii";
            this.userdefinedCheck.UseVisualStyleBackColor = true;
            this.userdefinedCheck.CheckedChanged += new System.EventHandler(this.userdefinedCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "From smallest to largest";
            // 
            // tori_simp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 338);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userdefinedCheck);
            this.Controls.Add(this.standardpipeCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.schedule_num);
            this.Controls.Add(this.outer_dia);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "tori_simp";
            this.Text = "Tori Simplification";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox schedule_num;
        private System.Windows.Forms.ComboBox outer_dia;
        private System.Windows.Forms.CheckBox standardpipeCheck;
        private System.Windows.Forms.CheckBox userdefinedCheck;
        private System.Windows.Forms.Label label1;
    }
}