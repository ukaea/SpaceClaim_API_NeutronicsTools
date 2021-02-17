namespace CCFE_Toolbox.CCFE_Commands
{
    partial class PipeRadiusOptions
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
            this.hollowCheck = new System.Windows.Forms.CheckBox();
            this.innerRadius = new System.Windows.Forms.NumericUpDown();
            this.outerRadius = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.okbutton = new System.Windows.Forms.Button();
            this.userdefinedCheck = new System.Windows.Forms.CheckBox();
            this.outer_dia = new System.Windows.Forms.ComboBox();
            this.schedule_num = new System.Windows.Forms.ComboBox();
            this.standardpipeCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.colour_dropdown = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.innerRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outerRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // hollowCheck
            // 
            this.hollowCheck.AutoSize = true;
            this.hollowCheck.Location = new System.Drawing.Point(153, 119);
            this.hollowCheck.Name = "hollowCheck";
            this.hollowCheck.Size = new System.Drawing.Size(82, 17);
            this.hollowCheck.TabIndex = 0;
            this.hollowCheck.Text = "Hollow Pipe";
            this.hollowCheck.UseVisualStyleBackColor = true;
            this.hollowCheck.CheckedChanged += new System.EventHandler(this.hollowCheck_CheckedChanged);
            // 
            // innerRadius
            // 
            this.innerRadius.DecimalPlaces = 5;
            this.innerRadius.Location = new System.Drawing.Point(152, 157);
            this.innerRadius.Name = "innerRadius";
            this.innerRadius.Size = new System.Drawing.Size(120, 20);
            this.innerRadius.TabIndex = 1;
            this.innerRadius.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // outerRadius
            // 
            this.outerRadius.DecimalPlaces = 5;
            this.outerRadius.Location = new System.Drawing.Point(12, 157);
            this.outerRadius.Name = "outerRadius";
            this.outerRadius.Size = new System.Drawing.Size(120, 20);
            this.outerRadius.TabIndex = 2;
            this.outerRadius.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Outer Radius (cm)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Inner Radius (cm)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // okbutton
            // 
            this.okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbutton.Location = new System.Drawing.Point(198, 238);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 5;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // userdefinedCheck
            // 
            this.userdefinedCheck.AutoSize = true;
            this.userdefinedCheck.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.userdefinedCheck.Location = new System.Drawing.Point(15, 120);
            this.userdefinedCheck.Name = "userdefinedCheck";
            this.userdefinedCheck.Size = new System.Drawing.Size(108, 17);
            this.userdefinedCheck.TabIndex = 6;
            this.userdefinedCheck.Text = "User defined radii";
            this.userdefinedCheck.UseVisualStyleBackColor = true;
            this.userdefinedCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.outer_dia.Location = new System.Drawing.Point(14, 88);
            this.outer_dia.Name = "outer_dia";
            this.outer_dia.Size = new System.Drawing.Size(117, 21);
            this.outer_dia.TabIndex = 7;
            this.outer_dia.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            this.schedule_num.Location = new System.Drawing.Point(153, 88);
            this.schedule_num.Name = "schedule_num";
            this.schedule_num.Size = new System.Drawing.Size(119, 21);
            this.schedule_num.TabIndex = 8;
            this.schedule_num.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // standardpipeCheck
            // 
            this.standardpipeCheck.AutoSize = true;
            this.standardpipeCheck.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.standardpipeCheck.Location = new System.Drawing.Point(15, 48);
            this.standardpipeCheck.Name = "standardpipeCheck";
            this.standardpipeCheck.Size = new System.Drawing.Size(93, 17);
            this.standardpipeCheck.TabIndex = 9;
            this.standardpipeCheck.Text = "Standard Pipe";
            this.standardpipeCheck.UseVisualStyleBackColor = true;
            this.standardpipeCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "DN Number";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Schedule Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "If no selection is made below the radius of the selected ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "pipe section will be used";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // colour_dropdown
            // 
            this.colour_dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colour_dropdown.FormattingEnabled = true;
            this.colour_dropdown.Location = new System.Drawing.Point(14, 212);
            this.colour_dropdown.Name = "colour_dropdown";
            this.colour_dropdown.Size = new System.Drawing.Size(121, 21);
            this.colour_dropdown.TabIndex = 14;
            this.colour_dropdown.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Pipe Colour";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // PipeRadiusOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 273);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.colour_dropdown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.standardpipeCheck);
            this.Controls.Add(this.schedule_num);
            this.Controls.Add(this.outer_dia);
            this.Controls.Add(this.userdefinedCheck);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outerRadius);
            this.Controls.Add(this.innerRadius);
            this.Controls.Add(this.hollowCheck);
            this.Name = "PipeRadiusOptions";
            this.Text = "Pipe Radii";
            this.Load += new System.EventHandler(this.PipeRadiusOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.innerRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outerRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox hollowCheck;
        private System.Windows.Forms.NumericUpDown innerRadius;
        private System.Windows.Forms.NumericUpDown outerRadius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.CheckBox userdefinedCheck;
        private System.Windows.Forms.ComboBox outer_dia;
        private System.Windows.Forms.ComboBox schedule_num;
        private System.Windows.Forms.CheckBox standardpipeCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox colour_dropdown;
        private System.Windows.Forms.Label label7;
    }
}