using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyThread
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            //为了显示到rtb控件，防止捕获异步线程调用错误。
            CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 同步测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Sync_Click(object sender, EventArgs e)
        {
            rtb_Sync.Clear();
            rtb_Sync.AppendText(string.Format("当前同步方法开始 ID：{0} \n", Thread.CurrentThread.ManagedThreadId));
            var j = 0;
            var k = 1;
            var m = j + k;

            for (int i = 0; i < 5; i++)
            {
                var name = string.Format("{0}_{1}", "btn_Sync_Click", i);
                DoSomethingLong(name, rtb_Sync);
            }
            rtb_Sync.AppendText(string.Format("当前同步方法结束 ID：{0}\n ", Thread.CurrentThread.ManagedThreadId));
        }

        /// <summary>
        /// 异步测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Async_Click(object sender, EventArgs e)
        {
            rtb_Async.Clear();
            rtb_Async.AppendText(string.Format("当前异步方法开始 ID：{0} ", Thread.CurrentThread.ManagedThreadId));
            Action<string, RichTextBox> action = DoSomethingLong;
            action.Invoke("btn_Async_Click_1", rtb_Async);
            action("btn_Async_Click_2", rtb_Async);
            action.BeginInvoke("btn_Async_Click_3", rtb_Async, null, null);//异步
            rtb_Async.AppendText(string.Format("当前异步方法结束 ID：{0} ", Thread.CurrentThread.ManagedThreadId));
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="name"></param>
        private void DoSomethingLong(string name, RichTextBox rtb)
        {
            rtb.AppendText(string.Format("DoSomethingLong方法开始，按钮名称：{0} 线程ID： {1} 开始时间： {2} \n", name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff")));

            var result = 0;
            for (int i = 0; i < 100000; i++)
            {
                result += i;
            }
            rtb.AppendText(string.Format("DoSomethingLong方法结束，按钮名称：{0} 线程ID： {1} 开始时间： {2} \n", name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff")));
        }
    }
}
