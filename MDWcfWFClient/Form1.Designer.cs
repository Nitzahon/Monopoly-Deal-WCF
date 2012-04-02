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
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
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
            this.listBoxPlayer0Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer1Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer2Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer3Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer4Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer0Bank = new System.Windows.Forms.ListBox();
            this.listBoxPlayer1Bank = new System.Windows.Forms.ListBox();
            this.listBoxPlayer2Bank = new System.Windows.Forms.ListBox();
            this.listBoxPlayer3Bank = new System.Windows.Forms.ListBox();
            this.listBoxPlayer4Bank = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.listBoxPSetsP0 = new System.Windows.Forms.ListBox();
            this.listBoxPSetsP1 = new System.Windows.Forms.ListBox();
            this.listBoxPSetsP2 = new System.Windows.Forms.ListBox();
            this.listBoxPSetsP3 = new System.Windows.Forms.ListBox();
            this.listBoxPSetsP4 = new System.Windows.Forms.ListBox();
            this.listBoxPSetSelectedP0 = new System.Windows.Forms.ListBox();
            this.listBoxPSetSelectedP1 = new System.Windows.Forms.ListBox();
            this.listBoxPSetSelectedP2 = new System.Windows.Forms.ListBox();
            this.listBoxPSetSelectedP3 = new System.Windows.Forms.ListBox();
            this.listBoxPSetSelectedP4 = new System.Windows.Forms.ListBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 433);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "MyHand";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(661, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "MyPropertiesP3";
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(661, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "MyPropertiesP1";
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(661, 318);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "MyPropertiesP4";
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
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(661, 387);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 50;
            this.label18.Text = "MyPropertiesP5";
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
            // buttonPoll
            // 
            this.buttonPoll.Enabled = false;
            this.buttonPoll.Location = new System.Drawing.Point(12, 87);
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
            this.button3.Location = new System.Drawing.Point(12, 399);
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
            this.buttonBankCard.Location = new System.Drawing.Point(12, 363);
            this.buttonBankCard.Name = "buttonBankCard";
            this.buttonBankCard.Size = new System.Drawing.Size(203, 23);
            this.buttonBankCard.TabIndex = 59;
            this.buttonBankCard.Text = "Bank Card From Hand";
            this.buttonBankCard.UseVisualStyleBackColor = true;
            this.buttonBankCard.Click += new System.EventHandler(this.buttonBankCard_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 23);
            this.button1.TabIndex = 60;
            this.button1.Text = "Discard Card From Hand";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBoxPlayer0Hand
            // 
            this.listBoxPlayer0Hand.FormattingEnabled = true;
            this.listBoxPlayer0Hand.Location = new System.Drawing.Point(422, 130);
            this.listBoxPlayer0Hand.Name = "listBoxPlayer0Hand";
            this.listBoxPlayer0Hand.Size = new System.Drawing.Size(174, 43);
            this.listBoxPlayer0Hand.TabIndex = 61;
            // 
            // listBoxPlayer1Hand
            // 
            this.listBoxPlayer1Hand.FormattingEnabled = true;
            this.listBoxPlayer1Hand.Location = new System.Drawing.Point(422, 202);
            this.listBoxPlayer1Hand.Name = "listBoxPlayer1Hand";
            this.listBoxPlayer1Hand.Size = new System.Drawing.Size(174, 43);
            this.listBoxPlayer1Hand.TabIndex = 62;
            // 
            // listBoxPlayer2Hand
            // 
            this.listBoxPlayer2Hand.FormattingEnabled = true;
            this.listBoxPlayer2Hand.Location = new System.Drawing.Point(422, 266);
            this.listBoxPlayer2Hand.Name = "listBoxPlayer2Hand";
            this.listBoxPlayer2Hand.Size = new System.Drawing.Size(174, 43);
            this.listBoxPlayer2Hand.TabIndex = 63;
            // 
            // listBoxPlayer3Hand
            // 
            this.listBoxPlayer3Hand.FormattingEnabled = true;
            this.listBoxPlayer3Hand.Location = new System.Drawing.Point(422, 334);
            this.listBoxPlayer3Hand.Name = "listBoxPlayer3Hand";
            this.listBoxPlayer3Hand.Size = new System.Drawing.Size(174, 43);
            this.listBoxPlayer3Hand.TabIndex = 64;
            // 
            // listBoxPlayer4Hand
            // 
            this.listBoxPlayer4Hand.FormattingEnabled = true;
            this.listBoxPlayer4Hand.Location = new System.Drawing.Point(422, 403);
            this.listBoxPlayer4Hand.Name = "listBoxPlayer4Hand";
            this.listBoxPlayer4Hand.Size = new System.Drawing.Size(174, 43);
            this.listBoxPlayer4Hand.TabIndex = 65;
            // 
            // listBoxPlayer0Bank
            // 
            this.listBoxPlayer0Bank.FormattingEnabled = true;
            this.listBoxPlayer0Bank.Location = new System.Drawing.Point(232, 130);
            this.listBoxPlayer0Bank.Name = "listBoxPlayer0Bank";
            this.listBoxPlayer0Bank.Size = new System.Drawing.Size(179, 43);
            this.listBoxPlayer0Bank.TabIndex = 66;
            // 
            // listBoxPlayer1Bank
            // 
            this.listBoxPlayer1Bank.FormattingEnabled = true;
            this.listBoxPlayer1Bank.Location = new System.Drawing.Point(232, 202);
            this.listBoxPlayer1Bank.Name = "listBoxPlayer1Bank";
            this.listBoxPlayer1Bank.Size = new System.Drawing.Size(179, 43);
            this.listBoxPlayer1Bank.TabIndex = 67;
            // 
            // listBoxPlayer2Bank
            // 
            this.listBoxPlayer2Bank.FormattingEnabled = true;
            this.listBoxPlayer2Bank.Location = new System.Drawing.Point(232, 266);
            this.listBoxPlayer2Bank.Name = "listBoxPlayer2Bank";
            this.listBoxPlayer2Bank.Size = new System.Drawing.Size(179, 43);
            this.listBoxPlayer2Bank.TabIndex = 68;
            // 
            // listBoxPlayer3Bank
            // 
            this.listBoxPlayer3Bank.FormattingEnabled = true;
            this.listBoxPlayer3Bank.Location = new System.Drawing.Point(232, 334);
            this.listBoxPlayer3Bank.Name = "listBoxPlayer3Bank";
            this.listBoxPlayer3Bank.Size = new System.Drawing.Size(179, 43);
            this.listBoxPlayer3Bank.TabIndex = 69;
            // 
            // listBoxPlayer4Bank
            // 
            this.listBoxPlayer4Bank.FormattingEnabled = true;
            this.listBoxPlayer4Bank.Location = new System.Drawing.Point(232, 403);
            this.listBoxPlayer4Bank.Name = "listBoxPlayer4Bank";
            this.listBoxPlayer4Bank.Size = new System.Drawing.Size(179, 43);
            this.listBoxPlayer4Bank.TabIndex = 70;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 23);
            this.button2.TabIndex = 71;
            this.button2.Text = "Has Game Started";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // listBoxPSetsP0
            // 
            this.listBoxPSetsP0.FormattingEnabled = true;
            this.listBoxPSetsP0.Location = new System.Drawing.Point(616, 131);
            this.listBoxPSetsP0.Name = "listBoxPSetsP0";
            this.listBoxPSetsP0.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetsP0.TabIndex = 72;
            // 
            // listBoxPSetsP1
            // 
            this.listBoxPSetsP1.FormattingEnabled = true;
            this.listBoxPSetsP1.Location = new System.Drawing.Point(616, 202);
            this.listBoxPSetsP1.Name = "listBoxPSetsP1";
            this.listBoxPSetsP1.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetsP1.TabIndex = 73;
            // 
            // listBoxPSetsP2
            // 
            this.listBoxPSetsP2.FormattingEnabled = true;
            this.listBoxPSetsP2.Location = new System.Drawing.Point(616, 266);
            this.listBoxPSetsP2.Name = "listBoxPSetsP2";
            this.listBoxPSetsP2.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetsP2.TabIndex = 74;
            // 
            // listBoxPSetsP3
            // 
            this.listBoxPSetsP3.FormattingEnabled = true;
            this.listBoxPSetsP3.Location = new System.Drawing.Point(616, 334);
            this.listBoxPSetsP3.Name = "listBoxPSetsP3";
            this.listBoxPSetsP3.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetsP3.TabIndex = 75;
            // 
            // listBoxPSetsP4
            // 
            this.listBoxPSetsP4.FormattingEnabled = true;
            this.listBoxPSetsP4.Location = new System.Drawing.Point(616, 403);
            this.listBoxPSetsP4.Name = "listBoxPSetsP4";
            this.listBoxPSetsP4.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetsP4.TabIndex = 76;
            // 
            // listBoxPSetSelectedP0
            // 
            this.listBoxPSetSelectedP0.FormattingEnabled = true;
            this.listBoxPSetSelectedP0.Location = new System.Drawing.Point(778, 131);
            this.listBoxPSetSelectedP0.Name = "listBoxPSetSelectedP0";
            this.listBoxPSetSelectedP0.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetSelectedP0.TabIndex = 77;
            // 
            // listBoxPSetSelectedP1
            // 
            this.listBoxPSetSelectedP1.FormattingEnabled = true;
            this.listBoxPSetSelectedP1.Location = new System.Drawing.Point(778, 202);
            this.listBoxPSetSelectedP1.Name = "listBoxPSetSelectedP1";
            this.listBoxPSetSelectedP1.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetSelectedP1.TabIndex = 78;
            // 
            // listBoxPSetSelectedP2
            // 
            this.listBoxPSetSelectedP2.FormattingEnabled = true;
            this.listBoxPSetSelectedP2.Location = new System.Drawing.Point(778, 266);
            this.listBoxPSetSelectedP2.Name = "listBoxPSetSelectedP2";
            this.listBoxPSetSelectedP2.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetSelectedP2.TabIndex = 79;
            // 
            // listBoxPSetSelectedP3
            // 
            this.listBoxPSetSelectedP3.FormattingEnabled = true;
            this.listBoxPSetSelectedP3.Location = new System.Drawing.Point(778, 334);
            this.listBoxPSetSelectedP3.Name = "listBoxPSetSelectedP3";
            this.listBoxPSetSelectedP3.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetSelectedP3.TabIndex = 80;
            // 
            // listBoxPSetSelectedP4
            // 
            this.listBoxPSetSelectedP4.FormattingEnabled = true;
            this.listBoxPSetSelectedP4.Location = new System.Drawing.Point(778, 403);
            this.listBoxPSetSelectedP4.Name = "listBoxPSetSelectedP4";
            this.listBoxPSetSelectedP4.Size = new System.Drawing.Size(156, 43);
            this.listBoxPSetSelectedP4.TabIndex = 81;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(779, 112);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 13);
            this.label21.TabIndex = 82;
            this.label21.Text = "Cards in selected Set";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(779, 178);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(107, 13);
            this.label22.TabIndex = 83;
            this.label22.Text = "Cards in selected Set";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(779, 248);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(107, 13);
            this.label23.TabIndex = 84;
            this.label23.Text = "Cards in selected Set";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(779, 318);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(107, 13);
            this.label24.TabIndex = 85;
            this.label24.Text = "Cards in selected Set";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(779, 380);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(107, 13);
            this.label25.TabIndex = 86;
            this.label25.Text = "Cards in selected Set";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 305);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 23);
            this.button4.TabIndex = 87;
            this.button4.Text = "Play Property To New Set";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 276);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(197, 23);
            this.button5.TabIndex = 88;
            this.button5.Text = "Play Property To Selected Set";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 536);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.listBoxPSetSelectedP4);
            this.Controls.Add(this.listBoxPSetSelectedP3);
            this.Controls.Add(this.listBoxPSetSelectedP2);
            this.Controls.Add(this.listBoxPSetSelectedP1);
            this.Controls.Add(this.listBoxPSetSelectedP0);
            this.Controls.Add(this.listBoxPSetsP4);
            this.Controls.Add(this.listBoxPSetsP3);
            this.Controls.Add(this.listBoxPSetsP2);
            this.Controls.Add(this.listBoxPSetsP1);
            this.Controls.Add(this.listBoxPSetsP0);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBoxPlayer4Bank);
            this.Controls.Add(this.listBoxPlayer3Bank);
            this.Controls.Add(this.listBoxPlayer2Bank);
            this.Controls.Add(this.listBoxPlayer1Bank);
            this.Controls.Add(this.listBoxPlayer0Bank);
            this.Controls.Add(this.listBoxPlayer4Hand);
            this.Controls.Add(this.listBoxPlayer3Hand);
            this.Controls.Add(this.listBoxPlayer2Hand);
            this.Controls.Add(this.listBoxPlayer1Hand);
            this.Controls.Add(this.listBoxPlayer0Hand);
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
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
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
        private System.Windows.Forms.ListBox listBoxPlayer0Hand;
        private System.Windows.Forms.ListBox listBoxPlayer1Hand;
        private System.Windows.Forms.ListBox listBoxPlayer2Hand;
        private System.Windows.Forms.ListBox listBoxPlayer3Hand;
        private System.Windows.Forms.ListBox listBoxPlayer4Hand;
        private System.Windows.Forms.ListBox listBoxPlayer0Bank;
        private System.Windows.Forms.ListBox listBoxPlayer1Bank;
        private System.Windows.Forms.ListBox listBoxPlayer2Bank;
        private System.Windows.Forms.ListBox listBoxPlayer3Bank;
        private System.Windows.Forms.ListBox listBoxPlayer4Bank;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBoxPSetsP0;
        private System.Windows.Forms.ListBox listBoxPSetsP1;
        private System.Windows.Forms.ListBox listBoxPSetsP2;
        private System.Windows.Forms.ListBox listBoxPSetsP3;
        private System.Windows.Forms.ListBox listBoxPSetsP4;
        private System.Windows.Forms.ListBox listBoxPSetSelectedP0;
        private System.Windows.Forms.ListBox listBoxPSetSelectedP1;
        private System.Windows.Forms.ListBox listBoxPSetSelectedP2;
        private System.Windows.Forms.ListBox listBoxPSetSelectedP3;
        private System.Windows.Forms.ListBox listBoxPSetSelectedP4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

