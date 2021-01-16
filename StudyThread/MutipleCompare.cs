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
    public partial class MutipleCompare : Form
    {
        public MutipleCompare()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_Compare_Click(object sender, EventArgs e)
        {
            rtb_CompareText.Clear();
            rtb_CompareText.AppendText(string.Format("btn_Compare_Click异步方法开始 ID：{0} \n", Thread.CurrentThread.ManagedThreadId));

            #region .NetFramework 1.0 1.1 Thread
            //优点：程序员能自由的调用丰富的Thread API
            //缺点：Thread各种方法调用因为线程由操作系统分配，响应不是非常及时，控制性不强，造成各种问题。
            rtb_CompareText.AppendText(".NetFramework 1.0 1.1 Thread \n");
            ThreadStart threadStart = () =>
            {
                rtb_CompareText.AppendText(string.Format("Thread ID：{0} 开始 \n", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(1000);
                rtb_CompareText.AppendText(string.Format("Thread ID：{0} 结束 \n", Thread.CurrentThread.ManagedThreadId));
            };

            Thread thread = new Thread(threadStart);
            thread.Start();

            #endregion

            #region .NetFramework 2.0 ThreadPool
            //池化资源管理设计思想，每次调用需要申请新线程，用完就释放。
            //避免频繁的申请和销毁，
            //线程池会提前申请5个线程，
            //程序使用时直接找线程池获取，
            //用完后放回线程池中，
            //线程池还会根据限制的数量去申请和释放。
            //优点：线程服用，限制最大线程数量。
            //缺点：线程等待顺序控制特别弱，API太少。
            rtb_CompareText.AppendText(".NetFramework 2.0 ThreadPool \n");
            WaitCallback callback = p =>
            {
                rtb_CompareText.AppendText(string.Format("ThreadPool调用 ID：{0} 开始 \n", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(1000);
                rtb_CompareText.AppendText(string.Format("ThreadPool调用 ID：{0} 结束 \n", Thread.CurrentThread.ManagedThreadId));
            };

            ThreadPool.QueueUserWorkItem(callback, null);
            #endregion

            #region .NetFramework 3.0   Task（最佳）
            //Task线程全是线程池线程
            //提供了丰富的API
            rtb_CompareText.AppendText(".NetFramework 3.0   Task \n");
            Action action = () =>
            {
                rtb_CompareText.AppendText(string.Format("Task调用 ID：{0} 开始 \n", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(1000);
                rtb_CompareText.AppendText(string.Format("Task调用 ID：{0} 结束 \n", Thread.CurrentThread.ManagedThreadId));
            };

            var task = new Task(action);
            task.Start();

            #endregion


            #region Parallel
            //Parallel可以启动多线程，主线程也参与计算，节约一个线程。
            //可以通过 ParallelOptions控制最大并发数量。
            Parallel.Invoke(() =>
            {
                rtb_CompareText.AppendText(string.Format("Parallel调用 ID：{0} 开始1 \n", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(1000);
                rtb_CompareText.AppendText(string.Format("Parallel调用 ID：{0} 结束1 \n", Thread.CurrentThread.ManagedThreadId));

            }, () =>
            {
                rtb_CompareText.AppendText(string.Format("Parallel调用 ID：{0} 开始2 \n", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(1000);
                rtb_CompareText.AppendText(string.Format("Parallel调用 ID：{0} 结束2 \n", Thread.CurrentThread.ManagedThreadId));

            }, () =>
            {
                rtb_CompareText.AppendText(string.Format("Parallel调用 ID：{0} 开始3 \n", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(1000);
                rtb_CompareText.AppendText(string.Format("Parallel调用 ID：{0} 结束3 \n", Thread.CurrentThread.ManagedThreadId));

            });
            #endregion

            #region await async


            #endregion

            rtb_CompareText.AppendText(string.Format("btn_Compare_Click异步方法结束 ID：{0} \n", Thread.CurrentThread.ManagedThreadId));
        }
    }
}
