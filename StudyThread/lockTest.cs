using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyThread
{
    public class lockTest
    {

        private static readonly object LockObj = new object();

        private readonly object LockObjTemp = new object();

        private readonly string LockObjString = "测试lock字符串";
        public static void ShowTest(System.Windows.Forms.RichTextBox rtb_CompareText)
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (LockObj)
                    {
                        rtb_CompareText.AppendText(string.Format("lockTest.ShowTest当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId));
                        Thread.Sleep(2000);
                        rtb_CompareText.AppendText(string.Format("lockTest.ShowTest当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId));
                    }
                });
            }

        }


        public void ShowTemp(System.Windows.Forms.RichTextBox rtb_CompareText, int index)
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (LockObjTemp)
                    {
                        rtb_CompareText.AppendText(string.Format("lockTest.ShowTemp{3}当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                        Thread.Sleep(2000);
                        rtb_CompareText.AppendText(string.Format("lockTest.ShowTemp{3}当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                    }
                });
            }

        }


        public void ShowString(System.Windows.Forms.RichTextBox rtb_CompareText, int index)
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (LockObjString)
                    {
                        rtb_CompareText.AppendText(string.Format("lockTest.ShowString{3}当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                        Thread.Sleep(2000);
                        rtb_CompareText.AppendText(string.Format("lockTest.ShowString{3}当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                    }
                });
            }

        }

        private int _num = 0;
        public void ShowThisAnother(System.Windows.Forms.RichTextBox rtb_CompareText, int index)
        {
            for (int i = 0; i < 5; i++)
            {
                _num++;
                int k = i;
                lock (this)
                {
                    rtb_CompareText.AppendText(string.Format("lockTest.ShowThisAnother{3}当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                    Thread.Sleep(2000);
                    rtb_CompareText.AppendText(string.Format("lockTest.ShowThisAnother{3}当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                    if (this._num < 5)
                    {
                        this.ShowThisAnother(rtb_CompareText, index);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }


    public class lockTestGeneric<T>
    {

        private static readonly object LockObj = new object();
        public static void ShowGeneric(System.Windows.Forms.RichTextBox rtb_CompareText, int index)
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (LockObj)
                    {
                        rtb_CompareText.AppendText(string.Format("lockTestGeneric.ShowGeneric{3}当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                        Thread.Sleep(2000);
                        rtb_CompareText.AppendText(string.Format("lockTestGeneric.ShowGeneric{3}当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId, index));
                    }
                });
            }


        }
    }
}
