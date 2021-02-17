namespace CCFE_Toolbox.UI
{
    partial class Pipes_Radius
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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.outerRadius = new System.Windows.Forms.NumericUpDown();
            this.innerRadius = new System.Windows.Forms.NumericUpDown();
            this.hollowCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.outerRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.innerRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(217, 100);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Inner Radius (m)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Outer Radius (m)";
            // 
            // outerRadius
            // 
            this.outerRadius.DecimalPlaces = 5;
            this.outerRadius.Location = new System.Drawing.Point(32, 74);
            this.outerRadius.Name = "outerRadius";
            this.outerRadius.Size = new System.Drawing.Size(120, 20);
            this.outerRadius.TabIndex = 14;
            // 
            // innerRadius
            // 
            this.innerRadius.DecimalPlaces = 5;
            this.innerRadius.Location = new System.Drawing.Point(172, 74);
            this.innerRadius.Name = "innerRadius";
            this.innerRadius.Size = new System.Drawing.Size(120, 20);
            this.innerRadius.TabIndex = 13;
            // 
            // hollowCheck
            // 
            this.hollowCheck.AutoSize = true;
            this.hollowCheck.Location = new System.Drawing.Point(173, 38);
            this.hollowCheck.Name = "hollowCheck";
            this.hollowCheck.Size = new System.Drawing.Size(82, 17);
            this.hollowCheck.TabIndex = 12;
            this.hollowCheck.Text = "Hollow Pipe";
            this.hollowCheck.UseVisualStyleBackColor = true;
            this.hollowCheck.CheckedChanged += new System.EventHandler(this.hollowCheck_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(85, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Error finding pipe radius. ";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Please enter user defined values below";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Pipes_Radius
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 134);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outerRadius);
            this.Controls.Add(this.innerRadius);
            this.Controls.Add(this.hollowCheck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Pipes_Radius";
            this.Text = "Error Finding Radius";
            ((System.ComponentModel.ISupportInitialize)(this.outerRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.innerRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown outerRadius;
        private System.Windows.Forms.NumericUpDown innerRadius;
        private System.Windows.Forms.CheckBox hollowCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}