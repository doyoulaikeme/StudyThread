
namespace StudyThread
{
    partial class ThreadMain
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
            this.btn_Thread = new System.Windows.Forms.Button();
            this.btn_MutipleCompare = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Thread
            // 
            this.btn_Thread.Location = new System.Drawing.Point(142, 157);
            this.btn_Thread.Name = "btn_Thread";
            this.btn_Thread.Size = new System.Drawing.Size(140, 23);
            this.btn_Thread.TabIndex = 0;
            this.btn_Thread.Text = "多线程与单线程";
            this.btn_Thread.UseVisualStyleBackColor = true;
            this.btn_Thread.Click += new System.EventHandler(this.btn_Thread_Click);
            // 
            // btn_MutipleCompare
            // 
            this.btn_MutipleCompare.Location = new System.Drawing.Point(375, 157);
            this.btn_MutipleCompare.Name = "btn_MutipleCompare";
            this.btn_MutipleCompare.Size = new System.Drawing.Size(170, 23);
            this.btn_MutipleCompare.TabIndex = 1;
            this.btn_MutipleCompare.Text = "不同版本线程比较";
            this.btn_MutipleCompare.UseVisualStyleBackColor = true;
            this.btn_MutipleCompare.Click += new System.EventHandler(this.btn_MutipleCompare_Click);
            // 
            // ThreadMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 399);
            this.Controls.Add(this.btn_MutipleCompare);
            this.Controls.Add(this.btn_Thread);
            this.Name = "ThreadMain";
            this.Text = "ThreadMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Thread;
        private System.Windows.Forms.Button btn_MutipleCompare;
    }
}