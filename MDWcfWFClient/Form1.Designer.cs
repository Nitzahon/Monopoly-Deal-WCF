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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.textBoxMyHand = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMyBank = new System.Windows.Forms.TextBox();
            this.textBoxMyProperties = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxProp2 = new System.Windows.Forms.TextBox();
            this.textBoxBank2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxHand2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxProp3 = new System.Windows.Forms.TextBox();
            this.textBoxBank3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxHand3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxProp1 = new System.Windows.Forms.TextBox();
            this.textBoxBank1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxHand1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxProp4 = new System.Windows.Forms.TextBox();
            this.textBoxBank4 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxHand4 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxProp5 = new System.Windows.Forms.TextBox();
            this.textBoxBank5 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxHand5 = new System.Windows.Forms.TextBox();
            this.buttonPoll = new System.Windows.Forms.Button();
            this.buttonDraw2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonRearrange = new System.Windows.Forms.Button();
            this.buttonEndTurn = new System.Windows.Forms.Button();
            this.buttonSelectOption = new System.Windows.Forms.Button();
            this.buttonJustSayNo = new System.Windows.Forms.Button();
            this.buttonBankCard = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 37);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Player Name";
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Location = new System.Drawing.Point(85, 14);
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.Size = new System.Drawing.Size(133, 20);
            this.textBoxPlayerName.TabIndex = 6;
            this.textBoxPlayerName.Text = "Player1";
            this.textBoxPlayerName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Enabled = false;
            this.buttonStartGame.Location = new System.Drawing.Point(121, 37);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(97, 23);
            this.buttonStartGame.TabIndex = 7;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Status";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(279, 16);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(493, 94);
            this.textBoxLog.TabIndex = 10;
            // 
            // textBoxMyHand
            // 
            this.textBoxMyHand.Location = new System.Drawing.Point(417, 481);
            this.textBoxMyHand.Multiline = true;
            this.textBoxMyHand.Name = "textBoxMyHand";
            this.textBoxMyHand.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMyHand.Size = new System.Drawing.Size(179, 43);
            this.textBoxMyHand.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 465);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "MyHand";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 465);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "MyBank";
            // 
            // textBoxMyBank
            // 
            this.textBoxMyBank.Location = new System.Drawing.Point(232, 481);
            this.textBoxMyBank.Multiline = true;
            this.textBoxMyBank.Name = "textBoxMyBank";
            this.textBoxMyBank.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMyBank.Size = new System.Drawing.Size(179, 43);
            this.textBoxMyBank.TabIndex = 18;
            // 
            // textBoxMyProperties
            // 
            this.textBoxMyProperties.Location = new System.Drawing.Point(602, 481);
            this.textBoxMyProperties.Multiline = true;
            this.textBoxMyProperties.Name = "textBoxMyProperties";
            this.textBoxMyProperties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMyProperties.Size = new System.Drawing.Size(179, 43);
            this.textBoxMyProperties.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(661, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "MyProperties";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(661, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "MyPropertiesP2";
            // 
            // textBoxProp2
            // 
            this.textBoxProp2.Location = new System.Drawing.Point(602, 204);
            this.textBoxProp2.Multiline = true;
            this.textBoxProp2.Name = "textBoxProp2";
            this.textBoxProp2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProp2.Size = new System.Drawing.Size(179, 43);
            this.textBoxProp2.TabIndex = 25;
            // 
            // textBoxBank2
            // 
            this.textBoxBank2.Location = new System.Drawing.Point(232, 204);
            this.textBoxBank2.Multiline = true;
            this.textBoxBank2.Name = "textBoxBank2";
            this.textBoxBank2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBank2.Size = new System.Drawing.Size(179, 43);
            this.textBoxBank2.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(286, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "MyBankP2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(482, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "MyHandP2";
            // 
            // textBoxHand2
            // 
            this.textBoxHand2.Location = new System.Drawing.Point(417, 204);
            this.textBoxHand2.Multiline = true;
            this.textBoxHand2.Name = "textBoxHand2";
            this.textBoxHand2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHand2.Size = new System.Drawing.Size(179, 43);
            this.textBoxHand2.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(661, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "MyPropertiesP3";
            // 
            // textBoxProp3
            // 
            this.textBoxProp3.Location = new System.Drawing.Point(602, 266);
            this.textBoxProp3.Multiline = true;
            this.textBoxProp3.Name = "textBoxProp3";
            this.textBoxProp3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProp3.Size = new System.Drawing.Size(179, 43);
            this.textBoxProp3.TabIndex = 31;
            // 
            // textBoxBank3
            // 
            this.textBoxBank3.Location = new System.Drawing.Point(232, 266);
            this.textBoxBank3.Multiline = true;
            this.textBoxBank3.Name = "textBoxBank3";
            this.textBoxBank3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBank3.Size = new System.Drawing.Size(179, 43);
            this.textBoxBank3.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(286, 250);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "MyBankP3";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(482, 250);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "MyHandP3";
            // 
            // textBoxHand3
            // 
            this.textBoxHand3.Location = new System.Drawing.Point(417, 266);
            this.textBoxHand3.Multiline = true;
            this.textBoxHand3.Name = "textBoxHand3";
            this.textBoxHand3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHand3.Size = new System.Drawing.Size(179, 43);
            this.textBoxHand3.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(661, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "MyPropertiesP1";
            // 
            // textBoxProp1
            // 
            this.textBoxProp1.Location = new System.Drawing.Point(602, 131);
            this.textBoxProp1.Multiline = true;
            this.textBoxProp1.Name = "textBoxProp1";
            this.textBoxProp1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProp1.Size = new System.Drawing.Size(179, 43);
            this.textBoxProp1.TabIndex = 37;
            // 
            // textBoxBank1
            // 
            this.textBoxBank1.Location = new System.Drawing.Point(232, 131);
            this.textBoxBank1.Multiline = true;
            this.textBoxBank1.Name = "textBoxBank1";
            this.textBoxBank1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBank1.Size = new System.Drawing.Size(179, 43);
            this.textBoxBank1.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(286, 115);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "MyBankP1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(482, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "MyHandP1";
            // 
            // textBoxHand1
            // 
            this.textBoxHand1.Location = new System.Drawing.Point(417, 131);
            this.textBoxHand1.Multiline = true;
            this.textBoxHand1.Name = "textBoxHand1";
            this.textBoxHand1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHand1.Size = new System.Drawing.Size(179, 43);
            this.textBoxHand1.TabIndex = 33;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(661, 318);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "MyPropertiesP4";
            // 
            // textBoxProp4
            // 
            this.textBoxProp4.Location = new System.Drawing.Point(602, 334);
            this.textBoxProp4.Multiline = true;
            this.textBoxProp4.Name = "textBoxProp4";
            this.textBoxProp4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProp4.Size = new System.Drawing.Size(179, 43);
            this.textBoxProp4.TabIndex = 43;
            // 
            // textBoxBank4
            // 
            this.textBoxBank4.Location = new System.Drawing.Point(232, 334);
            this.textBoxBank4.Multiline = true;
            this.textBoxBank4.Name = "textBoxBank4";
            this.textBoxBank4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBank4.Size = new System.Drawing.Size(179, 43);
            this.textBoxBank4.TabIndex = 42;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(286, 318);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 41;
            this.label16.Text = "MyBankP4";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(482, 318);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 40;
            this.label17.Text = "MyHandP4";
            // 
            // textBoxHand4
            // 
            this.textBoxHand4.Location = new System.Drawing.Point(417, 334);
            this.textBoxHand4.Multiline = true;
            this.textBoxHand4.Name = "textBoxHand4";
            this.textBoxHand4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHand4.Size = new System.Drawing.Size(179, 43);
            this.textBoxHand4.TabIndex = 39;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(661, 387);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 50;
            this.label18.Text = "MyPropertiesP5";
            // 
            // textBoxProp5
            // 
            this.textBoxProp5.Location = new System.Drawing.Point(602, 403);
            this.textBoxProp5.Multiline = true;
            this.textBoxProp5.Name = "textBoxProp5";
            this.textBoxProp5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProp5.Size = new System.Drawing.Size(179, 43);
            this.textBoxProp5.TabIndex = 49;
            // 
            // textBoxBank5
            // 
            this.textBoxBank5.Location = new System.Drawing.Point(232, 403);
            this.textBoxBank5.Multiline = true;
            this.textBoxBank5.Name = "textBoxBank5";
            this.textBoxBank5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBank5.Size = new System.Drawing.Size(179, 43);
            this.textBoxBank5.TabIndex = 48;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(286, 387);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "MyBankP5";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(482, 387);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 13);
            this.label20.TabIndex = 46;
            this.label20.Text = "MyHandP5";
            // 
            // textBoxHand5
            // 
            this.textBoxHand5.Location = new System.Drawing.Point(417, 403);
            this.textBoxHand5.Multiline = true;
            this.textBoxHand5.Name = "textBoxHand5";
            this.textBoxHand5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHand5.Size = new System.Drawing.Size(179, 43);
            this.textBoxHand5.TabIndex = 45;
            // 
            // buttonPoll
            // 
            this.buttonPoll.Location = new System.Drawing.Point(12, 66);
            this.buttonPoll.Name = "buttonPoll";
            this.buttonPoll.Size = new System.Drawing.Size(203, 23);
            this.buttonPoll.TabIndex = 51;
            this.buttonPoll.Text = "PollState";
            this.buttonPoll.UseVisualStyleBackColor = true;
            this.buttonPoll.Click += new System.EventHandler(this.buttonPoll_Click);
            // 
            // buttonDraw2
            // 
            this.buttonDraw2.Location = new System.Drawing.Point(15, 115);
            this.buttonDraw2.Name = "buttonDraw2";
            this.buttonDraw2.Size = new System.Drawing.Size(203, 23);
            this.buttonDraw2.TabIndex = 52;
            this.buttonDraw2.Text = "Draw 2 Cards at Turn Start";
            this.buttonDraw2.UseVisualStyleBackColor = true;
            this.buttonDraw2.Click += new System.EventHandler(this.buttonDraw2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 423);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(203, 23);
            this.button3.TabIndex = 53;
            this.button3.Text = "Play Card From Hand";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 455);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(203, 69);
            this.listBox1.TabIndex = 54;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonRearrange
            // 
            this.buttonRearrange.Location = new System.Drawing.Point(15, 144);
            this.buttonRearrange.Name = "buttonRearrange";
            this.buttonRearrange.Size = new System.Drawing.Size(203, 23);
            this.buttonRearrange.TabIndex = 55;
            this.buttonRearrange.Text = "Rearrange Properties";
            this.buttonRearrange.UseVisualStyleBackColor = true;
            // 
            // buttonEndTurn
            // 
            this.buttonEndTurn.Location = new System.Drawing.Point(15, 173);
            this.buttonEndTurn.Name = "buttonEndTurn";
            this.buttonEndTurn.Size = new System.Drawing.Size(203, 23);
            this.buttonEndTurn.TabIndex = 56;
            this.buttonEndTurn.Text = "End Turn";
            this.buttonEndTurn.UseVisualStyleBackColor = true;
            // 
            // buttonSelectOption
            // 
            this.buttonSelectOption.Location = new System.Drawing.Point(15, 202);
            this.buttonSelectOption.Name = "buttonSelectOption";
            this.buttonSelectOption.Size = new System.Drawing.Size(203, 23);
            this.buttonSelectOption.TabIndex = 57;
            this.buttonSelectOption.Text = "Select Option";
            this.buttonSelectOption.UseVisualStyleBackColor = true;
            // 
            // buttonJustSayNo
            // 
            this.buttonJustSayNo.Location = new System.Drawing.Point(15, 231);
            this.buttonJustSayNo.Name = "buttonJustSayNo";
            this.buttonJustSayNo.Size = new System.Drawing.Size(203, 23);
            this.buttonJustSayNo.TabIndex = 58;
            this.buttonJustSayNo.Text = "Use Just Say No!";
            this.buttonJustSayNo.UseVisualStyleBackColor = true;
            // 
            // buttonBankCard
            // 
            this.buttonBankCard.Location = new System.Drawing.Point(12, 387);
            this.buttonBankCard.Name = "buttonBankCard";
            this.buttonBankCard.Size = new System.Drawing.Size(203, 23);
            this.buttonBankCard.TabIndex = 59;
            this.buttonBankCard.Text = "Bank Card From Hand";
            this.buttonBankCard.UseVisualStyleBackColor = true;
            this.buttonBankCard.Click += new System.EventHandler(this.buttonBankCard_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 23);
            this.button1.TabIndex = 60;
            this.button1.Text = "Discard Card From Hand";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 536);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBankCard);
            this.Controls.Add(this.buttonJustSayNo);
            this.Controls.Add(this.buttonSelectOption);
            this.Controls.Add(this.buttonEndTurn);
            this.Controls.Add(this.buttonRearrange);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonDraw2);
            this.Controls.Add(this.buttonPoll);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBoxProp5);
            this.Controls.Add(this.textBoxBank5);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.textBoxHand5);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBoxProp4);
            this.Controls.Add(this.textBoxBank4);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBoxHand4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxProp1);
            this.Controls.Add(this.textBoxBank1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxHand1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxProp3);
            this.Controls.Add(this.textBoxBank3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxHand3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxProp2);
            this.Controls.Add(this.textBoxBank2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxHand2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMyProperties);
            this.Controls.Add(this.textBoxMyBank);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMyHand);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.textBoxPlayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "Monopoly Deal Windows Form Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button buttonConnect;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox textBoxPlayerName;
        internal System.Windows.Forms.Button buttonStartGame;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.TextBox textBoxMyHand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMyBank;
        private System.Windows.Forms.TextBox textBoxMyProperties;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxProp2;
        private System.Windows.Forms.TextBox textBoxBank2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxHand2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxProp3;
        private System.Windows.Forms.TextBox textBoxBank3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxHand3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxProp1;
        private System.Windows.Forms.TextBox textBoxBank1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxHand1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxProp4;
        private System.Windows.Forms.TextBox textBoxBank4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxHand4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxProp5;
        private System.Windows.Forms.TextBox textBoxBank5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxHand5;
        private System.Windows.Forms.Button buttonPoll;
        private System.Windows.Forms.Button buttonDraw2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonRearrange;
        private System.Windows.Forms.Button buttonEndTurn;
        private System.Windows.Forms.Button buttonSelectOption;
        private System.Windows.Forms.Button buttonJustSayNo;
        private System.Windows.Forms.Button buttonBankCard;
        private System.Windows.Forms.Button button1;
    }
}

