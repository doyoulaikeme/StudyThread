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
            action.Invoke("btn_Async_Click_1", rtb_Async);//单线程同步模式
            action("btn_Async_Click_2", rtb_Async);//单线程同步模式
            action.BeginInvoke("btn_Async_Click_3", rtb_Async, null, null);//多线程异步模式
            rtb_Async.AppendText(string.Format("当前异步方法结束 ID：{0} ", Thread.CurrentThread.ManagedThreadId));
        }

        /// <summary>
        /// 异步进阶方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AsyncAdvanced_Click(object sender, EventArgs e)
        {
            rtb_Async.Clear();
            rtb_Async.AppendText(string.Format("当前异步进阶方法开始 ID：{0} \n ", Thread.CurrentThread.ManagedThreadId));


            #region 异步回调方法
            //模拟数据库操作后要记录日志
            action.Invoke("btn_AsyncAdvanced_Click_1", rtb_Async);//单线程同步模式
            //多线程异步完成后回调该方法
            Action<string, RichTextBox> action = UpdateDB;
            AsyncCallback callback = ar =>
            {

                rtb_Async.AppendText(string.Format("当前异步进阶方法已完成！ID：{0} 回调后的属性值：{1} ", Thread.CurrentThread.ManagedThreadId, ar.AsyncState));
            };
            action.BeginInvoke("btn_AsyncAdvanced_Click_3", rtb_Async, callback, "已完成");//多线程异步模式 
            #endregion

            #region IsCompleted等待操作完成
            ////模拟用户必须确定操作完成，才能返回，例如 上传文件成功后才能浏览。
            //Action<string, RichTextBox> action = UploadFile;
            //var asyncResult = action.BeginInvoke("文件上传", rtb_Async, null, null);
            //var i = 0;
            //while (!asyncResult.IsCompleted)
            //{
            //    if (i < 10)
            //    {
            //        rtb_Async.AppendText(string.Format("进度：{0}%  \n", ++i * 10));
            //    }
            //    else
            //    {
            //        rtb_Async.AppendText(string.Format("进度：99%  \n"));
            //    }

            //    Thread.Sleep(10);
            //}
            //rtb_Async.AppendText(string.Format("完成文件上传，可以预览！ ID：{0} ", Thread.CurrentThread.ManagedThreadId));
            #endregion

            #region 信号量
            //Action<string, RichTextBox> action = UpdateDB;
            //var asyncResult = action.BeginInvoke("调用接口", rtb_Async, null, null);

            ////asyncResult.AsyncWaitHandle.WaitOne();//阻塞当前线程，直到收到信号量，从asyncResult发出。无延迟

            ////asyncResult.AsyncWaitHandle.WaitOne(-1);//一直等待，直到收到信号量

            //asyncResult.AsyncWaitHandle.WaitOne(100);//最多等待100ms



            //rtb_Async.AppendText(string.Format("接口调用成功！ ID：{0} \n", Thread.CurrentThread.ManagedThreadId));
            #endregion

            #region EndInvoke方法获取真实返回值
            //Func<int> fuc = RemoteService;
            //var asyncResult = fuc.BeginInvoke(ar =>
            //{
            //    ////回调与主线程EndInvoke只能调用一个，多个会发生错误。
            //    //var callBackResult = fuc.EndInvoke(ar);
            //    //rtb_Async.AppendText(string.Format("回调EndInvoke值：{0} \n", callBackResult));
            //}, null);

            //var result = fuc.EndInvoke(asyncResult);//主线程获取异步调用的真实返回值
            //rtb_Async.AppendText(string.Format("主线程EndInvoke值：{0} \n", result));

            #endregion


            rtb_Async.AppendText(string.Format("当前异步进阶方法结束 ID：{0} ", Thread.CurrentThread.ManagedThreadId));
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


        /// <summary>
        /// 异步进阶测试方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rtb"></param>
        private void UpdateDB(string name, RichTextBox rtb)
        {
            rtb.AppendText(string.Format("UpdateDB方法开始，按钮名称：{0} 线程ID： {1} 开始时间： {2} \n", name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff")));

            var result = 0;
            for (int i = 0; i < 100000; i++)
            {
                result += i;
            }
            rtb.AppendText(string.Format("UpdateDB方法结束，按钮名称：{0} 线程ID： {1} 开始时间： {2} \n", name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff")));
        }

        /// <summary>
        /// 模拟文件上传
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rtb"></param>
        private void UploadFile(string name, RichTextBox rtb)
        {
            var result = 0;
            for (int i = 0; i < 100000; i++)
            {
                result += i;
            }
        }

        /// <summary>
        /// 模拟远程服务
        /// </summary>
        /// <returns></returns>
        private int RemoteService()
        {

            var result = 0;
            for (int i = 0; i < 100000; i++)
            {
                result += i;
            }

            return result;
        }
    }
}
