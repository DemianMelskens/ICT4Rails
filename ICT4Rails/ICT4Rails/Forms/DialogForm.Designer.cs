namespace ICT4Rails.Forms
{
    partial class DialogForm
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
            this.pl_Form_Total_Context = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pAddMoveDelete = new System.Windows.Forms.Panel();
            this.tbTramID = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpBeginDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblBeginDate = new System.Windows.Forms.Label();
            this.tbSegmentID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pStatusOverview = new System.Windows.Forms.Panel();
            this.cbTramStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTramStatusID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pl_Form_Total_Context.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pAddMoveDelete.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pStatusOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_Form_Total_Context
            // 
            this.pl_Form_Total_Context.Controls.Add(this.panel2);
            this.pl_Form_Total_Context.Controls.Add(this.pAddMoveDelete);
            this.pl_Form_Total_Context.Controls.Add(this.panel3);
            this.pl_Form_Total_Context.Controls.Add(this.pStatusOverview);
            this.pl_Form_Total_Context.Location = new System.Drawing.Point(3, 3);
            this.pl_Form_Total_Context.Name = "pl_Form_Total_Context";
            this.pl_Form_Total_Context.Size = new System.Drawing.Size(441, 359);
            this.pl_Form_Total_Context.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSubmit);
            this.panel2.Location = new System.Drawing.Point(3, 301);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 55);
            this.panel2.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(228, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(190, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(18, 13);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(190, 30);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pAddMoveDelete
            // 
            this.pAddMoveDelete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pAddMoveDelete.Controls.Add(this.tbTramID);
            this.pAddMoveDelete.Controls.Add(this.dtpEndDate);
            this.pAddMoveDelete.Controls.Add(this.dtpBeginDate);
            this.pAddMoveDelete.Controls.Add(this.lblEndDate);
            this.pAddMoveDelete.Controls.Add(this.lblBeginDate);
            this.pAddMoveDelete.Controls.Add(this.tbSegmentID);
            this.pAddMoveDelete.Controls.Add(this.label2);
            this.pAddMoveDelete.Controls.Add(this.label20);
            this.pAddMoveDelete.Location = new System.Drawing.Point(3, 90);
            this.pAddMoveDelete.Name = "pAddMoveDelete";
            this.pAddMoveDelete.Size = new System.Drawing.Size(435, 205);
            this.pAddMoveDelete.TabIndex = 4;
            // 
            // tbTramID
            // 
            this.tbTramID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbTramID.FormattingEnabled = true;
            this.tbTramID.Location = new System.Drawing.Point(101, 37);
            this.tbTramID.Name = "tbTramID";
            this.tbTramID.Size = new System.Drawing.Size(235, 21);
            this.tbTramID.TabIndex = 16;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(236, 167);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(107, 20);
            this.dtpEndDate.TabIndex = 15;
            this.dtpEndDate.Visible = false;
            // 
            // dtpBeginDate
            // 
            this.dtpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBeginDate.Location = new System.Drawing.Point(101, 167);
            this.dtpBeginDate.Name = "dtpBeginDate";
            this.dtpBeginDate.Size = new System.Drawing.Size(107, 20);
            this.dtpBeginDate.TabIndex = 14;
            this.dtpBeginDate.Visible = false;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(232, 141);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(90, 20);
            this.lblEndDate.TabIndex = 13;
            this.lblEndDate.Text = "End Date:";
            this.lblEndDate.Visible = false;
            // 
            // lblBeginDate
            // 
            this.lblBeginDate.AutoSize = true;
            this.lblBeginDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeginDate.Location = new System.Drawing.Point(102, 141);
            this.lblBeginDate.Name = "lblBeginDate";
            this.lblBeginDate.Size = new System.Drawing.Size(104, 20);
            this.lblBeginDate.TabIndex = 12;
            this.lblBeginDate.Text = "Begin Date:";
            this.lblBeginDate.Visible = false;
            // 
            // tbSegmentID
            // 
            this.tbSegmentID.Location = new System.Drawing.Point(101, 100);
            this.tbSegmentID.Name = "tbSegmentID";
            this.tbSegmentID.Size = new System.Drawing.Size(235, 20);
            this.tbSegmentID.TabIndex = 6;
            this.tbSegmentID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Segment ID:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(97, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(78, 20);
            this.label20.TabIndex = 3;
            this.label20.Text = "Tram ID:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(435, 81);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "EyeCT4Rails";
            // 
            // pStatusOverview
            // 
            this.pStatusOverview.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pStatusOverview.Controls.Add(this.cbTramStatus);
            this.pStatusOverview.Controls.Add(this.label3);
            this.pStatusOverview.Controls.Add(this.tbTramStatusID);
            this.pStatusOverview.Controls.Add(this.label4);
            this.pStatusOverview.Location = new System.Drawing.Point(4, 90);
            this.pStatusOverview.Name = "pStatusOverview";
            this.pStatusOverview.Size = new System.Drawing.Size(434, 205);
            this.pStatusOverview.TabIndex = 7;
            // 
            // cbTramStatus
            // 
            this.cbTramStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTramStatus.FormattingEnabled = true;
            this.cbTramStatus.Items.AddRange(new object[] {
            "ReadyForUse",
            "NeedsCleaning",
            "NeedsReperation",
            "InRemise",
            "Defect",
            "GeenStatusBekent"});
            this.cbTramStatus.Location = new System.Drawing.Point(101, 131);
            this.cbTramStatus.Name = "cbTramStatus";
            this.cbTramStatus.Size = new System.Drawing.Size(235, 21);
            this.cbTramStatus.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(97, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Status:";
            // 
            // tbTramStatusID
            // 
            this.tbTramStatusID.Location = new System.Drawing.Point(101, 68);
            this.tbTramStatusID.Name = "tbTramStatusID";
            this.tbTramStatusID.Size = new System.Drawing.Size(235, 20);
            this.tbTramStatusID.TabIndex = 4;
            this.tbTramStatusID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(97, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tram ID:";
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(446, 365);
            this.Controls.Add(this.pl_Form_Total_Context);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogForm";
            this.pl_Form_Total_Context.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pAddMoveDelete.ResumeLayout(false);
            this.pAddMoveDelete.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pStatusOverview.ResumeLayout(false);
            this.pStatusOverview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_Form_Total_Context;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pAddMoveDelete;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbSegmentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel pStatusOverview;
        private System.Windows.Forms.ComboBox cbTramStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTramStatusID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpBeginDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblBeginDate;
        private System.Windows.Forms.ComboBox tbTramID;
    }
}