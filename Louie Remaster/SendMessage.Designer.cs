namespace Louie_Remaster
{
    partial class SendMessage
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
            this.gbTerminal = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblSendMessage = new System.Windows.Forms.Label();
            this.cbSelectChannel = new System.Windows.Forms.ComboBox();
            this.lblChannelID = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.gbTerminal.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTerminal
            // 
            this.gbTerminal.BackColor = System.Drawing.Color.Transparent;
            this.gbTerminal.Controls.Add(this.btnSend);
            this.gbTerminal.Controls.Add(this.txtMessage);
            this.gbTerminal.Location = new System.Drawing.Point(13, 169);
            this.gbTerminal.Name = "gbTerminal";
            this.gbTerminal.Size = new System.Drawing.Size(893, 463);
            this.gbTerminal.TabIndex = 3;
            this.gbTerminal.TabStop = false;
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
            // lblSendMessage
            // 
            this.lblSendMessage.AutoSize = true;
            this.lblSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold);
            this.lblSendMessage.Location = new System.Drawing.Point(291, 9);
            this.lblSendMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSendMessage.Name = "lblSendMessage";
            this.lblSendMessage.Size = new System.Drawing.Size(347, 54);
            this.lblSendMessage.TabIndex = 4;
            this.lblSendMessage.Text = "Send Message";
            // 
            // cbSelectChannel
            // 
            this.cbSelectChannel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSelectChannel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSelectChannel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbSelectChannel.FormattingEnabled = true;
            this.cbSelectChannel.Location = new System.Drawing.Point(258, 67);
            this.cbSelectChannel.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectChannel.Name = "cbSelectChannel";
            this.cbSelectChannel.Size = new System.Drawing.Size(411, 37);
            this.cbSelectChannel.TabIndex = 5;
            this.cbSelectChannel.SelectedIndexChanged += new System.EventHandler(this.cbSelectChannel_SelectedIndexChanged);
            // 
            // lblChannelID
            // 
            this.lblChannelID.AutoSize = true;
            this.lblChannelID.Location = new System.Drawing.Point(410, 127);
            this.lblChannelID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChannelID.Name = "lblChannelID";
            this.lblChannelID.Size = new System.Drawing.Size(89, 17);
            this.lblChannelID.TabIndex = 6;
            this.lblChannelID.Text = "[#CHANNEL]";
            this.lblChannelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(148, 22);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(595, 291);
            this.txtMessage.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(148, 352);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(595, 61);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send Message";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // SendMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 653);
            this.Controls.Add(this.lblChannelID);
            this.Controls.Add(this.cbSelectChannel);
            this.Controls.Add(this.lblSendMessage);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.gbTerminal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(936, 700);
            this.MinimumSize = new System.Drawing.Size(936, 694);
            this.Name = "SendMessage";
            this.Text = "Send Message";
            this.Load += new System.EventHandler(this.SendMessage_Load);
            this.gbTerminal.ResumeLayout(false);
            this.gbTerminal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbTerminal;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnSend;
        internal System.Windows.Forms.TextBox txtMessage;
        internal System.Windows.Forms.Label lblSendMessage;
        internal System.Windows.Forms.ComboBox cbSelectChannel;
        internal System.Windows.Forms.Label lblChannelID;
    }
}