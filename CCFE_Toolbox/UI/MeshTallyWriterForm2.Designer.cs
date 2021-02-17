namespace CCFE_Toolbox.UI
{
    partial class MeshTallyWriterForm2
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownx = new System.Windows.Forms.NumericUpDown();
            this.numericUpDowny = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownz = new System.Windows.Forms.NumericUpDown();
            this.labelt = new System.Windows.Forms.Label();
            this.labelx = new System.Windows.Forms.Label();
            this.labely = new System.Windows.Forms.Label();
            this.labelz = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDowny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownz)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(788, 248);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 35);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(666, 248);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 35);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(628, 53);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 28);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(507, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Particle Type";
            // 
            // numericUpDownt
            // 
            this.numericUpDownt.Location = new System.Drawing.Point(269, 55);
            this.numericUpDownt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownt.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownt.Name = "numericUpDownt";
            this.numericUpDownt.Size = new System.Drawing.Size(116, 26);
            this.numericUpDownt.TabIndex = 16;
            this.numericUpDownt.ValueChanged += new System.EventHandler(this.numericUpDownt_ValueChanged);
            // 
            // numericUpDownx
            // 
            this.numericUpDownx.DecimalPlaces = 3;
            this.numericUpDownx.Location = new System.Drawing.Point(107, 179);
            this.numericUpDownx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownx.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownx.Name = "numericUpDownx";
            this.numericUpDownx.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownx.TabIndex = 16;
            this.numericUpDownx.ValueChanged += new System.EventHandler(this.numericUpDownx_ValueChanged);
            // 
            // numericUpDowny
            // 
            this.numericUpDowny.DecimalPlaces = 3;
            this.numericUpDowny.Location = new System.Drawing.Point(344, 179);
            this.numericUpDowny.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDowny.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDowny.Name = "numericUpDowny";
            this.numericUpDowny.Size = new System.Drawing.Size(180, 26);
            this.numericUpDowny.TabIndex = 16;
            this.numericUpDowny.ValueChanged += new System.EventHandler(this.numericUpDowny_ValueChanged);
            // 
            // numericUpDownz
            // 
            this.numericUpDownz.DecimalPlaces = 3;
            this.numericUpDownz.Location = new System.Drawing.Point(579, 179);
            this.numericUpDownz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownz.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownz.Name = "numericUpDownz";
            this.numericUpDownz.Size = new System.Drawing.Size(180, 26);
            this.numericUpDownz.TabIndex = 16;
            this.numericUpDownz.ValueChanged += new System.EventHandler(this.numericUpDownz_ValueChanged);
            // 
            // labelt
            // 
            this.labelt.AutoSize = true;
            this.labelt.Location = new System.Drawing.Point(156, 56);
            this.labelt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelt.Name = "labelt";
            this.labelt.Size = new System.Drawing.Size(100, 20);
            this.labelt.TabIndex = 19;
            this.labelt.Text = "Tally Number";
            // 
            // labelx
            // 
            this.labelx.AutoSize = true;
            this.labelx.Location = new System.Drawing.Point(143, 151);
            this.labelx.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelx.Name = "labelx";
            this.labelx.Size = new System.Drawing.Size(100, 20);
            this.labelx.TabIndex = 19;
            this.labelx.Text = "X Resolution";
            // 
            // labely
            // 
            this.labely.AutoSize = true;
            this.labely.Location = new System.Drawing.Point(380, 151);
            this.labely.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labely.Name = "labely";
            this.labely.Size = new System.Drawing.Size(100, 20);
            this.labely.TabIndex = 19;
            this.labely.Text = "Y Resolution";
            // 
            // labelz
            // 
            this.labelz.AutoSize = true;
            this.labelz.Location = new System.Drawing.Point(621, 151);
            this.labelz.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelz.Name = "labelz";
            this.labelz.Size = new System.Drawing.Size(99, 20);
            this.labelz.TabIndex = 19;
            this.labelz.Text = "Z Resolution";
            // 
            // MeshTallyWriterForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 302);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.numericUpDownt);
            this.Controls.Add(this.numericUpDownx);
            this.Controls.Add(this.numericUpDowny);
            this.Controls.Add(this.numericUpDownz);
            this.Controls.Add(this.labelt);
            this.Controls.Add(this.labelx);
            this.Controls.Add(this.labely);
            this.Controls.Add(this.labelz);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MeshTallyWriterForm2";
            this.ShowIcon = false;
            this.Text = "Mesh Tally Writer";
            this.Load += new System.EventHandler(this.MeshTallyWriterForm2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDowny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownt;
        private System.Windows.Forms.NumericUpDown numericUpDownx;
        private System.Windows.Forms.NumericUpDown numericUpDowny;
        private System.Windows.Forms.NumericUpDown numericUpDownz;
        private System.Windows.Forms.Label labelt;
        private System.Windows.Forms.Label labelx;
        private System.Windows.Forms.Label labely;
        private System.Windows.Forms.Label labelz;
    }
}