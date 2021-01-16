
namespace StudyThread
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Sync = new System.Windows.Forms.Button();
            this.btn_Async = new System.Windows.Forms.Button();
            this.rtb_Sync = new System.Windows.Forms.RichTextBox();
            this.rtb_Async = new System.Windows.Forms.RichTextBox();
            this.btn_AsyncAdvanced = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Sync
            // 
            this.btn_Sync.Location = new System.Drawing.Point(124, 560);
            this.btn_Sync.Name = "btn_Sync";
            this.btn_Sync.Size = new System.Drawing.Size(75, 23);
            this.btn_Sync.TabIndex = 0;
            this.btn_Sync.Text = "同步方法";
            this.btn_Sync.UseVisualStyleBackColor = true;
            this.btn_Sync.Click += new System.EventHandler(this.btn_Sync_Click);
            // 
            // btn_Async
            // 
            this.btn_Async.Location = new System.Drawing.Point(720, 560);
            this.btn_Async.Name = "btn_Async";
            this.btn_Async.Size = new System.Drawing.Size(75, 23);
            this.btn_Async.TabIndex = 1;
            this.btn_Async.Text = "异步方法";
            this.btn_Async.UseVisualStyleBackColor = true;
            this.btn_Async.Click += new System.EventHandler(this.btn_Async_Click);
            // 
            // rtb_Sync
            // 
            this.rtb_Sync.Location = new System.Drawing.Point(-1, 0);
            this.rtb_Sync.Name = "rtb_Sync";
            this.rtb_Sync.Size = new System.Drawing.Size(586, 545);
            this.rtb_Sync.TabIndex = 2;
            this.rtb_Sync.Text = "";
            // 
            // rtb_Async
            // 
            this.rtb_Async.Location = new System.Drawing.Point(591, 0);
            this.rtb_Async.Name = "rtb_Async";
            this.rtb_Async.Size = new System.Drawing.Size(525, 545);
            this.rtb_Async.TabIndex = 3;
            this.rtb_Async.Text = "";
            // 
            // btn_AsyncAdvanced
            // 
            this.btn_AsyncAdvanced.Location = new System.Drawing.Point(886, 560);
            this.btn_AsyncAdvanced.Name = "btn_AsyncAdvanced";
            this.btn_AsyncAdvanced.Size = new System.Drawing.Size(107, 23);
            this.btn_AsyncAdvanced.TabIndex = 4;
            this.btn_AsyncAdvanced.Text = "异步进阶方法";
            this.btn_AsyncAdvanced.UseVisualStyleBackColor = true;
            this.btn_AsyncAdvanced.Click += new System.EventHandler(this.btn_AsyncAdvanced_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 633);
            this.Controls.Add(this.btn_AsyncAdvanced);
            this.Controls.Add(this.rtb_Async);
            this.Controls.Add(this.rtb_Sync);
            this.Controls.Add(this.btn_Async);
            this.Controls.Add(this.btn_Sync);
            this.Name = "Main";
            this.Text = "多线程研究";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Sync;
        private System.Windows.Forms.Button btn_Async;
        private System.Windows.Forms.RichTextBox rtb_Sync;
        private System.Windows.Forms.RichTextBox rtb_Async;
        private System.Windows.Forms.Button btn_AsyncAdvanced;
    }
}

