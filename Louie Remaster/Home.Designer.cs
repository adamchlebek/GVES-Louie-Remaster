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
            this.btnSendSetGame = new System.Windows.Forms.Button();
            this.btnTopSenders = new System.Windows.Forms.Button();
            this.btnMessagesPerRole = new System.Windows.Forms.Button();
            this.btnGetRoleList = new System.Windows.Forms.Button();
            this.btnReloadDatabase = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.gbTerminal.SuspendLayout();
            this.gbControls.SuspendLayout();
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
            this.gbControls.Controls.Add(this.btnSendSetGame);
            this.gbControls.Controls.Add(this.btnTopSenders);
            this.gbControls.Controls.Add(this.btnMessagesPerRole);
            this.gbControls.Controls.Add(this.btnGetRoleList);
            this.gbControls.Controls.Add(this.btnReloadDatabase);
            this.gbControls.Controls.Add(this.btnSendMessage);
            this.gbControls.Location = new System.Drawing.Point(13, 12);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(893, 107);
            this.gbControls.TabIndex = 2;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "Controls";
            // 
            // btnSendSetGame
            // 
            this.btnSendSetGame.Location = new System.Drawing.Point(300, 67);
            this.btnSendSetGame.Name = "btnSendSetGame";
            this.btnSendSetGame.Size = new System.Drawing.Size(141, 34);
            this.btnSendSetGame.TabIndex = 6;
            this.btnSendSetGame.Text = "Send Set Game";
            this.btnSendSetGame.UseVisualStyleBackColor = true;
            this.btnSendSetGame.Click += new System.EventHandler(this.btnSendSetGame_Click);
            // 
            // btnTopSenders
            // 
            this.btnTopSenders.Enabled = false;
            this.btnTopSenders.Location = new System.Drawing.Point(300, 27);
            this.btnTopSenders.Name = "btnTopSenders";
            this.btnTopSenders.Size = new System.Drawing.Size(141, 34);
            this.btnTopSenders.TabIndex = 5;
            this.btnTopSenders.Text = "Top Senders";
            this.btnTopSenders.UseVisualStyleBackColor = true;
            // 
            // btnMessagesPerRole
            // 
            this.btnMessagesPerRole.Enabled = false;
            this.btnMessagesPerRole.Location = new System.Drawing.Point(153, 67);
            this.btnMessagesPerRole.Name = "btnMessagesPerRole";
            this.btnMessagesPerRole.Size = new System.Drawing.Size(141, 34);
            this.btnMessagesPerRole.TabIndex = 4;
            this.btnMessagesPerRole.Text = "Messages Per Role";
            this.btnMessagesPerRole.UseVisualStyleBackColor = true;
            // 
            // btnGetRoleList
            // 
            this.btnGetRoleList.Enabled = false;
            this.btnGetRoleList.Location = new System.Drawing.Point(153, 27);
            this.btnGetRoleList.Name = "btnGetRoleList";
            this.btnGetRoleList.Size = new System.Drawing.Size(141, 34);
            this.btnGetRoleList.TabIndex = 3;
            this.btnGetRoleList.Text = "Get Role List";
            this.btnGetRoleList.UseVisualStyleBackColor = true;
            // 
            // btnReloadDatabase
            // 
            this.btnReloadDatabase.Location = new System.Drawing.Point(6, 67);
            this.btnReloadDatabase.Name = "btnReloadDatabase";
            this.btnReloadDatabase.Size = new System.Drawing.Size(141, 34);
            this.btnReloadDatabase.TabIndex = 2;
            this.btnReloadDatabase.Text = "Reload Database";
            this.btnReloadDatabase.UseVisualStyleBackColor = true;
            this.btnReloadDatabase.Click += new System.EventHandler(this.btnReloadDatabase_Click);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(6, 27);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(141, 34);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
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
            this.gbControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.GroupBox gbTerminal;
        private System.Windows.Forms.GroupBox gbControls;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnSendSetGame;
        private System.Windows.Forms.Button btnTopSenders;
        private System.Windows.Forms.Button btnMessagesPerRole;
        private System.Windows.Forms.Button btnGetRoleList;
        private System.Windows.Forms.Button btnReloadDatabase;
        private System.Windows.Forms.Button btnSendMessage;
    }
}

