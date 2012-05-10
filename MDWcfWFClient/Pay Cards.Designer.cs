namespace MDWcfWFClient
{
    partial class Pay_Cards
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxYourBank = new System.Windows.Forms.ListBox();
            this.listBoxYourProperties = new System.Windows.Forms.ListBox();
            this.listBoxCardsToUseToPay = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total amount to pay:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 90);
            this.button1.TabIndex = 1;
            this.button1.Text = "Just Say No";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Remaining:";
            // 
            // listBoxYourBank
            // 
            this.listBoxYourBank.FormattingEnabled = true;
            this.listBoxYourBank.Location = new System.Drawing.Point(12, 90);
            this.listBoxYourBank.Name = "listBoxYourBank";
            this.listBoxYourBank.Size = new System.Drawing.Size(197, 95);
            this.listBoxYourBank.TabIndex = 3;
            // 
            // listBoxYourProperties
            // 
            this.listBoxYourProperties.FormattingEnabled = true;
            this.listBoxYourProperties.Location = new System.Drawing.Point(12, 191);
            this.listBoxYourProperties.Name = "listBoxYourProperties";
            this.listBoxYourProperties.Size = new System.Drawing.Size(197, 95);
            this.listBoxYourProperties.TabIndex = 4;
            // 
            // listBoxCardsToUseToPay
            // 
            this.listBoxCardsToUseToPay.FormattingEnabled = true;
            this.listBoxCardsToUseToPay.Location = new System.Drawing.Point(346, 90);
            this.listBoxCardsToUseToPay.Name = "listBoxCardsToUseToPay";
            this.listBoxCardsToUseToPay.Size = new System.Drawing.Size(250, 199);
            this.listBoxCardsToUseToPay.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(215, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Add card in bank=>";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(215, 119);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "<=Remove Card";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(215, 191);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Add property card =>";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(215, 220);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "<= Remove Property Card";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(346, 295);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(250, 90);
            this.button6.TabIndex = 10;
            this.button6.Text = "Pay";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Total value of all your cards:";
            // 
            // Pay_Cards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 439);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBoxCardsToUseToPay);
            this.Controls.Add(this.listBoxYourProperties);
            this.Controls.Add(this.listBoxYourBank);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Pay_Cards";
            this.Text = "Pay_Cards";
            this.Load += new System.EventHandler(this.Pay_Cards_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxYourBank;
        private System.Windows.Forms.ListBox listBoxYourProperties;
        private System.Windows.Forms.ListBox listBoxCardsToUseToPay;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label3;
    }
}