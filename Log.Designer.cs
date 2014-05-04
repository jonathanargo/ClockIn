namespace ClockIn
{
    partial class Log
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
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.rbtEntries = new System.Windows.Forms.RadioButton();
            this.rbtSummary = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbxLog
            // 
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.Location = new System.Drawing.Point(12, 12);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(267, 225);
            this.lbxLog.TabIndex = 0;
            // 
            // rbtEntries
            // 
            this.rbtEntries.AutoSize = true;
            this.rbtEntries.Checked = true;
            this.rbtEntries.Location = new System.Drawing.Point(285, 12);
            this.rbtEntries.Name = "rbtEntries";
            this.rbtEntries.Size = new System.Drawing.Size(89, 17);
            this.rbtEntries.TabIndex = 1;
            this.rbtEntries.TabStop = true;
            this.rbtEntries.Text = "Single Entries";
            this.rbtEntries.UseVisualStyleBackColor = true;
            // 
            // rbtSummary
            // 
            this.rbtSummary.AutoSize = true;
            this.rbtSummary.Location = new System.Drawing.Point(285, 35);
            this.rbtSummary.Name = "rbtSummary";
            this.rbtSummary.Size = new System.Drawing.Size(68, 17);
            this.rbtSummary.TabIndex = 2;
            this.rbtSummary.Text = "Summary";
            this.rbtSummary.UseVisualStyleBackColor = true;
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 262);
            this.Controls.Add(this.rbtSummary);
            this.Controls.Add(this.rbtEntries);
            this.Controls.Add(this.lbxLog);
            this.Name = "Log";
            this.Text = "Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxLog;
        private System.Windows.Forms.RadioButton rbtEntries;
        private System.Windows.Forms.RadioButton rbtSummary;
    }
}