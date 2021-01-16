
namespace StudyThread
{
    partial class MutipleCompare
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
            this.btn_Compare = new System.Windows.Forms.Button();
            this.rtb_CompareText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_Compare
            // 
            this.btn_Compare.Location = new System.Drawing.Point(161, 543);
            this.btn_Compare.Name = "btn_Compare";
            this.btn_Compare.Size = new System.Drawing.Size(75, 23);
            this.btn_Compare.TabIndex = 0;
            this.btn_Compare.Text = "多线程比较";
            this.btn_Compare.UseVisualStyleBackColor = true;
            this.btn_Compare.Click += new System.EventHandler(this.btn_Compare_Click);
            // 
            // rtb_CompareText
            // 
            this.rtb_CompareText.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtb_CompareText.Location = new System.Drawing.Point(0, 0);
            this.rtb_CompareText.Name = "rtb_CompareText";
            this.rtb_CompareText.Size = new System.Drawing.Size(1119, 522);
            this.rtb_CompareText.TabIndex = 1;
            this.rtb_CompareText.Text = "";
            // 
            // MutipleCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 633);
            this.Controls.Add(this.rtb_CompareText);
            this.Controls.Add(this.btn_Compare);
            this.Name = "MutipleCompare";
            this.Text = "比较不同版本多线程";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Compare;
        private System.Windows.Forms.RichTextBox rtb_CompareText;
    }
}