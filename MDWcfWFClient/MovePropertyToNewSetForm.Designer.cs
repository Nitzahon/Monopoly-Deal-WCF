namespace MDWcfWFClient
{
    partial class MovePropertyToNewSetForm
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
            this.listBoxPickProperty = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPickOriginalSet = new System.Windows.Forms.Label();
            this.listBoxPickOriginalSetToRemoveCardFrom = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxCardsInSelectedSet = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxSetToMovePropertyTo = new System.Windows.Forms.ListBox();
            this.buttonMoveNew = new System.Windows.Forms.Button();
            this.buttonMoveSelected = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonMoveToSelectedSetDown = new System.Windows.Forms.Button();
            this.buttonMoveNewSetDown = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxPickProperty);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelPickOriginalSet);
            this.groupBox1.Controls.Add(this.listBoxPickOriginalSetToRemoveCardFrom);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pick Property Card To Move";
            // 
            // listBoxPickProperty
            // 
            this.listBoxPickProperty.FormattingEnabled = true;
            this.listBoxPickProperty.Location = new System.Drawing.Point(221, 32);
            this.listBoxPickProperty.Name = "listBoxPickProperty";
            this.listBoxPickProperty.Size = new System.Drawing.Size(208, 82);
            this.listBoxPickProperty.TabIndex = 5;
            this.listBoxPickProperty.SelectedIndexChanged += new System.EventHandler(this.listBoxPickProperty_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pick The Property Card to move.";
            // 
            // labelPickOriginalSet
            // 
            this.labelPickOriginalSet.AutoSize = true;
            this.labelPickOriginalSet.Location = new System.Drawing.Point(6, 16);
            this.labelPickOriginalSet.Name = "labelPickOriginalSet";
            this.labelPickOriginalSet.Size = new System.Drawing.Size(206, 13);
            this.labelPickOriginalSet.TabIndex = 3;
            this.labelPickOriginalSet.Text = "Pick the set to move a Property Card from.";
            // 
            // listBoxPickOriginalSetToRemoveCardFrom
            // 
            this.listBoxPickOriginalSetToRemoveCardFrom.FormattingEnabled = true;
            this.listBoxPickOriginalSetToRemoveCardFrom.Location = new System.Drawing.Point(6, 32);
            this.listBoxPickOriginalSetToRemoveCardFrom.Name = "listBoxPickOriginalSetToRemoveCardFrom";
            this.listBoxPickOriginalSetToRemoveCardFrom.Size = new System.Drawing.Size(206, 82);
            this.listBoxPickOriginalSetToRemoveCardFrom.TabIndex = 2;
            this.listBoxPickOriginalSetToRemoveCardFrom.SelectedIndexChanged += new System.EventHandler(this.listBoxPickOriginalSetToRemoveCardFrom_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listBoxCardsInSelectedSet);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.listBoxSetToMovePropertyTo);
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 127);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pick the Set to move the Property Card To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cards allready in selected set.";
            // 
            // listBoxCardsInSelectedSet
            // 
            this.listBoxCardsInSelectedSet.FormattingEnabled = true;
            this.listBoxCardsInSelectedSet.Location = new System.Drawing.Point(221, 39);
            this.listBoxCardsInSelectedSet.Name = "listBoxCardsInSelectedSet";
            this.listBoxCardsInSelectedSet.Size = new System.Drawing.Size(208, 82);
            this.listBoxCardsInSelectedSet.TabIndex = 2;
            this.listBoxCardsInSelectedSet.SelectedIndexChanged += new System.EventHandler(this.listBoxCardsInSelectedSet_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sets";
            // 
            // listBoxSetToMovePropertyTo
            // 
            this.listBoxSetToMovePropertyTo.FormattingEnabled = true;
            this.listBoxSetToMovePropertyTo.Location = new System.Drawing.Point(9, 39);
            this.listBoxSetToMovePropertyTo.Name = "listBoxSetToMovePropertyTo";
            this.listBoxSetToMovePropertyTo.Size = new System.Drawing.Size(203, 82);
            this.listBoxSetToMovePropertyTo.TabIndex = 0;
            this.listBoxSetToMovePropertyTo.SelectedIndexChanged += new System.EventHandler(this.listBoxSetToMovePropertyTo_SelectedIndexChanged);
            // 
            // buttonMoveNew
            // 
            this.buttonMoveNew.Location = new System.Drawing.Point(12, 284);
            this.buttonMoveNew.Name = "buttonMoveNew";
            this.buttonMoveNew.Size = new System.Drawing.Size(132, 25);
            this.buttonMoveNew.TabIndex = 2;
            this.buttonMoveNew.Text = "Move to new set Up";
            this.buttonMoveNew.UseVisualStyleBackColor = true;
            this.buttonMoveNew.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonMoveSelected
            // 
            this.buttonMoveSelected.Location = new System.Drawing.Point(150, 284);
            this.buttonMoveSelected.Name = "buttonMoveSelected";
            this.buttonMoveSelected.Size = new System.Drawing.Size(177, 25);
            this.buttonMoveSelected.TabIndex = 3;
            this.buttonMoveSelected.Text = "Move to Selected Set Up";
            this.buttonMoveSelected.UseVisualStyleBackColor = true;
            this.buttonMoveSelected.Click += new System.EventHandler(this.buttonMoveSelected_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(333, 284);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(121, 50);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonMoveToSelectedSetDown
            // 
            this.buttonMoveToSelectedSetDown.Location = new System.Drawing.Point(150, 311);
            this.buttonMoveToSelectedSetDown.Name = "buttonMoveToSelectedSetDown";
            this.buttonMoveToSelectedSetDown.Size = new System.Drawing.Size(177, 23);
            this.buttonMoveToSelectedSetDown.TabIndex = 5;
            this.buttonMoveToSelectedSetDown.Text = "Move To Selected Set Down";
            this.buttonMoveToSelectedSetDown.UseVisualStyleBackColor = true;
            this.buttonMoveToSelectedSetDown.Click += new System.EventHandler(this.buttonMoveToSelectedSetDown_Click);
            // 
            // buttonMoveNewSetDown
            // 
            this.buttonMoveNewSetDown.Location = new System.Drawing.Point(12, 311);
            this.buttonMoveNewSetDown.Name = "buttonMoveNewSetDown";
            this.buttonMoveNewSetDown.Size = new System.Drawing.Size(132, 23);
            this.buttonMoveNewSetDown.TabIndex = 6;
            this.buttonMoveNewSetDown.Text = "Move to new set Down";
            this.buttonMoveNewSetDown.UseVisualStyleBackColor = true;
            this.buttonMoveNewSetDown.Click += new System.EventHandler(this.buttonMoveNewSetDown_Click);
            // 
            // MovePropertyToNewSetForm
            // 
            this.AcceptButton = this.buttonMoveSelected;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(466, 346);
            this.Controls.Add(this.buttonMoveNewSetDown);
            this.Controls.Add(this.buttonMoveToSelectedSetDown);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonMoveSelected);
            this.Controls.Add(this.buttonMoveNew);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MovePropertyToNewSetForm";
            this.Text = "MovePropertyToNewSetForm";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxPickProperty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPickOriginalSet;
        private System.Windows.Forms.ListBox listBoxPickOriginalSetToRemoveCardFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxCardsInSelectedSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxSetToMovePropertyTo;
        private System.Windows.Forms.Button buttonMoveNew;
        private System.Windows.Forms.Button buttonMoveSelected;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonMoveToSelectedSetDown;
        private System.Windows.Forms.Button buttonMoveNewSetDown;
    }
}