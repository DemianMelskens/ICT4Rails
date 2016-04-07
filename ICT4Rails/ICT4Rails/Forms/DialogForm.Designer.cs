namespace ICT4Rails.Forms
{
    partial class FDialogForm
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
            this.SuspendLayout();
            // 
            // pl_Form_Total_Context
            // 
            this.pl_Form_Total_Context.Location = new System.Drawing.Point(3, 3);
            this.pl_Form_Total_Context.Name = "pl_Form_Total_Context";
            this.pl_Form_Total_Context.Size = new System.Drawing.Size(441, 373);
            this.pl_Form_Total_Context.TabIndex = 0;
            // 
            // FDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 378);
            this.Controls.Add(this.pl_Form_Total_Context);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FDialogForm";
            this.Text = "DialogForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_Form_Total_Context;
    }
}