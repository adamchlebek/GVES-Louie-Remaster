namespace Louie_Remaster
{
    partial class Home
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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.gbTerminal = new System.Windows.Forms.GroupBox();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.gbTerminal.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.Black;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.ForeColor = System.Drawing.Color.White;
            this.txtOutput.Location = new System.Drawing.Point(3, 18);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(887, 488);
            this.txtOutput.TabIndex = 0;
            // 
            // gbTerminal
            // 
            this.gbTerminal.BackColor = System.Drawing.Color.Transparent;
            this.gbTerminal.Controls.Add(this.txtOutput);
            this.gbTerminal.Location = new System.Drawing.Point(13, 125);
            this.gbTerminal.Name = "gbTerminal";
            this.gbTerminal.Size = new System.Drawing.Size(893, 509);
            this.gbTerminal.TabIndex = 3;
            this.gbTerminal.TabStop = false;
            // 
            // gbControls
            // 
            this.gbControls.Location = new System.Drawing.Point(13, 12);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(893, 107);
            this.gbControls.TabIndex = 2;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "Controls";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(16, 635);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(59, 17);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "v0.0.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 653);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.gbTerminal);
            this.Controls.Add(this.gbControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(936, 700);
            this.MinimumSize = new System.Drawing.Size(936, 694);
            this.Name = "Home";
            this.Text = "Louie";
            this.Load += new System.EventHandler(this.Home_Load);
            this.gbTerminal.ResumeLayout(false);
            this.gbTerminal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.GroupBox gbTerminal;
        private System.Windows.Forms.GroupBox gbControls;
        private System.Windows.Forms.Label lblVersion;
    }
}

