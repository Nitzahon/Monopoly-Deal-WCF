namespace MDWcfWFClient
{
    partial class UseActionCardForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUseDealBreaker = new System.Windows.Forms.Button();
            this.listBoxCardsInSet = new System.Windows.Forms.ListBox();
            this.listBoxPlayers = new System.Windows.Forms.ListBox();
            this.listBoxSets = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBoxCardToGiveUp = new System.Windows.Forms.ListBox();
            this.listBoxSetCardToGiveUpIsIn = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Controls.Add(this.buttonUseDealBreaker);
            this.groupBox1.Controls.Add(this.listBoxCardsInSet);
            this.groupBox1.Controls.Add(this.listBoxPlayers);
            this.groupBox1.Controls.Add(this.listBoxSets);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 282);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pick set to DealBreaker";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cards in Selected Set";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pick Set";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pick Player";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(10, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(211, 43);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonUseDealBreaker
            // 
            this.buttonUseDealBreaker.Location = new System.Drawing.Point(10, 166);
            this.buttonUseDealBreaker.Name = "buttonUseDealBreaker";
            this.buttonUseDealBreaker.Size = new System.Drawing.Size(211, 46);
            this.buttonUseDealBreaker.TabIndex = 1;
            this.buttonUseDealBreaker.Text = "DealBreaker selected set";
            this.buttonUseDealBreaker.UseVisualStyleBackColor = true;
            this.buttonUseDealBreaker.Click += new System.EventHandler(this.buttonUseDealBreaker_Click);
            // 
            // listBoxCardsInSet
            // 
            this.listBoxCardsInSet.FormattingEnabled = true;
            this.listBoxCardsInSet.Location = new System.Drawing.Point(238, 166);
            this.listBoxCardsInSet.Name = "listBoxCardsInSet";
            this.listBoxCardsInSet.Size = new System.Drawing.Size(215, 95);
            this.listBoxCardsInSet.TabIndex = 2;
            // 
            // listBoxPlayers
            // 
            this.listBoxPlayers.FormattingEnabled = true;
            this.listBoxPlayers.Location = new System.Drawing.Point(6, 40);
            this.listBoxPlayers.Name = "listBoxPlayers";
            this.listBoxPlayers.Size = new System.Drawing.Size(215, 95);
            this.listBoxPlayers.TabIndex = 1;
            this.listBoxPlayers.SelectedIndexChanged += new System.EventHandler(this.listBoxPlayers_SelectedIndexChanged);
            // 
            // listBoxSets
            // 
            this.listBoxSets.FormattingEnabled = true;
            this.listBoxSets.Location = new System.Drawing.Point(238, 40);
            this.listBoxSets.Name = "listBoxSets";
            this.listBoxSets.Size = new System.Drawing.Size(215, 95);
            this.listBoxSets.TabIndex = 0;
            this.listBoxSets.SelectedIndexChanged += new System.EventHandler(this.listBoxSets_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.listBoxCardToGiveUp);
            this.groupBox2.Controls.Add(this.listBoxSetCardToGiveUpIsIn);
            this.groupBox2.Location = new System.Drawing.Point(12, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 222);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pick card to give up in Forced Deal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cards in Selected Set";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Pick Set";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(227, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "Forced Deal Cards";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listBoxCardToGiveUp
            // 
            this.listBoxCardToGiveUp.FormattingEnabled = true;
            this.listBoxCardToGiveUp.Location = new System.Drawing.Point(240, 40);
            this.listBoxCardToGiveUp.Name = "listBoxCardToGiveUp";
            this.listBoxCardToGiveUp.Size = new System.Drawing.Size(215, 95);
            this.listBoxCardToGiveUp.TabIndex = 2;
            // 
            // listBoxSetCardToGiveUpIsIn
            // 
            this.listBoxSetCardToGiveUpIsIn.FormattingEnabled = true;
            this.listBoxSetCardToGiveUpIsIn.Location = new System.Drawing.Point(10, 40);
            this.listBoxSetCardToGiveUpIsIn.Name = "listBoxSetCardToGiveUpIsIn";
            this.listBoxSetCardToGiveUpIsIn.Size = new System.Drawing.Size(215, 95);
            this.listBoxSetCardToGiveUpIsIn.TabIndex = 0;
            this.listBoxSetCardToGiveUpIsIn.SelectedIndexChanged += new System.EventHandler(this.listBoxSetCardToGiveUpIsIn_SelectedIndexChanged);
            // 
            // UseActionCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(491, 297);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UseActionCardForm";
            this.Text = "DealBreaker";
            this.Load += new System.EventHandler(this.DealBreaker_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonUseDealBreaker;
        private System.Windows.Forms.ListBox listBoxCardsInSet;
        private System.Windows.Forms.ListBox listBoxPlayers;
        private System.Windows.Forms.ListBox listBoxSets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBoxCardToGiveUp;
        private System.Windows.Forms.ListBox listBoxSetCardToGiveUpIsIn;
    }
}