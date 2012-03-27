namespace MDWcfWFClient
{
    partial class Form1
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.serverIP = new System.Windows.Forms.TextBox();
            this.serverPortTB = new System.Windows.Forms.TextBox();
            this.updateServerAddress = new System.Windows.Forms.Button();
            this.textBoxPlayerList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.textBoxChatMessage = new System.Windows.Forms.TextBox();
            this.buttonChat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 87);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // serverIP
            // 
            this.serverIP.Location = new System.Drawing.Point(12, 12);
            this.serverIP.Name = "serverIP";
            this.serverIP.Size = new System.Drawing.Size(100, 20);
            this.serverIP.TabIndex = 1;
            this.serverIP.Text = "ServerIP";
            // 
            // serverPortTB
            // 
            this.serverPortTB.Location = new System.Drawing.Point(118, 12);
            this.serverPortTB.Name = "serverPortTB";
            this.serverPortTB.Size = new System.Drawing.Size(100, 20);
            this.serverPortTB.TabIndex = 2;
            this.serverPortTB.Text = "ServerPort";
            // 
            // updateServerAddress
            // 
            this.updateServerAddress.Location = new System.Drawing.Point(12, 38);
            this.updateServerAddress.Name = "updateServerAddress";
            this.updateServerAddress.Size = new System.Drawing.Size(206, 23);
            this.updateServerAddress.TabIndex = 3;
            this.updateServerAddress.Text = "Update Server Address";
            this.updateServerAddress.UseVisualStyleBackColor = true;
            // 
            // textBoxPlayerList
            // 
            this.textBoxPlayerList.Location = new System.Drawing.Point(12, 125);
            this.textBoxPlayerList.Name = "textBoxPlayerList";
            this.textBoxPlayerList.Size = new System.Drawing.Size(206, 20);
            this.textBoxPlayerList.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Player Name";
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Location = new System.Drawing.Point(85, 64);
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.Size = new System.Drawing.Size(133, 20);
            this.textBoxPlayerName.TabIndex = 6;
            this.textBoxPlayerName.Text = "Player1";
            this.textBoxPlayerName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Start Game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(118, 151);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Kick Player";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Status";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(279, 38);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(493, 392);
            this.textBoxLog.TabIndex = 10;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(118, 87);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(100, 23);
            this.buttonDisconnect.TabIndex = 11;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textBoxChat
            // 
            this.textBoxChat.Location = new System.Drawing.Point(15, 180);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.Size = new System.Drawing.Size(203, 153);
            this.textBoxChat.TabIndex = 12;
            // 
            // textBoxChatMessage
            // 
            this.textBoxChatMessage.Location = new System.Drawing.Point(15, 339);
            this.textBoxChatMessage.Name = "textBoxChatMessage";
            this.textBoxChatMessage.Size = new System.Drawing.Size(203, 20);
            this.textBoxChatMessage.TabIndex = 13;
            // 
            // buttonChat
            // 
            this.buttonChat.Location = new System.Drawing.Point(15, 365);
            this.buttonChat.Name = "buttonChat";
            this.buttonChat.Size = new System.Drawing.Size(203, 23);
            this.buttonChat.TabIndex = 14;
            this.buttonChat.Text = "Send";
            this.buttonChat.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.buttonChat);
            this.Controls.Add(this.textBoxChatMessage);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxPlayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPlayerList);
            this.Controls.Add(this.updateServerAddress);
            this.Controls.Add(this.serverPortTB);
            this.Controls.Add(this.serverIP);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "Monopoly Deal Windows Form Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox serverIP;
        private System.Windows.Forms.TextBox serverPortTB;
        private System.Windows.Forms.Button updateServerAddress;
        private System.Windows.Forms.TextBox textBoxPlayerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPlayerName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.TextBox textBoxChatMessage;
        private System.Windows.Forms.Button buttonChat;
    }
}

