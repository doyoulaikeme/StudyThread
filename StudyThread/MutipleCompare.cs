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

        private void btn_ThreadSafety_Click(object sender, EventArgs e)
        {
            rtb_CompareText.Clear();
            rtb_CompareText.AppendText(string.Format("btn_ThreadSafety_Click异步方法开始 ID：{0} \n", Thread.CurrentThread.ManagedThreadId));


            #region 线程安全问题 单线程与多线程
            //多线程安全问题：一段代码，由单线程跟多线程执行，结果不一致，就表明有线程安全问题。
            //List集合是个线性结构，在内存上是连续存储的，
            //添加一个数量就操作一个内存位置，如果多个cpu同时发出命令，
            //内存先执行一个再执行另一个，会出现覆盖，导致内存地址值发生改变。
            var iList = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                //单线程
                iList.Add(i);
            }
            rtb_CompareText.AppendText(string.Format("List集合单线程数量：{0} ID：{1} \n", iList.Count, Thread.CurrentThread.ManagedThreadId));

            iList = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                //多线程之后，结果数据会少于预计的数量，有些数据会丢失。
                Task.Run(() =>
                {
                    iList.Add(i);
                });
            }
            Thread.Sleep(200);
            rtb_CompareText.AppendText(string.Format("List集合多线程数量：{0} ID：{1} \n", iList.Count, Thread.CurrentThread.ManagedThreadId));

            #endregion

            #region 解决线程安全问题
            iList = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                //多线程之后，结果数据会少于预计的数量，有些数据会丢失。
                Task.Run(() =>
                {
                    //加lock能保证任意时刻只有一个线程进去，其他线程需要排队等待（单线程化）。
                    //lock原理=>Monitor----锁定一个内存引用地址，所以不能是值类型或null。
                    lock (obj)
                    {
                        iList.Add(i);
                    }

                });
            }
            Thread.Sleep(200);
            rtb_CompareText.AppendText(string.Format("List集合加锁多线程数量：{0} ID：{1} \n", iList.Count, Thread.CurrentThread.ManagedThreadId));
            #endregion

            #region lock测试

            lockTest.ShowTest(rtb_CompareText);
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    //加锁后 一个线程执行完然后才允许其他线程执行。
                    lock (obj)//共用同一个锁变量，会出现阻塞，如果需要并发，要定义不同的锁变量。
                    {
                        rtb_CompareText.AppendText(string.Format("ShowTest主线程当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId));
                        Thread.Sleep(2000);
                        rtb_CompareText.AppendText(string.Format("ShowTest主线程当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId));
                    }
                });
            }


            //实例化对象后调用能实现并发
            //是因为lock里面的引用地址经过实例化后会重新生成新的引用地址。
            //如果是static就不行，因为static是系统全局变量，
            //每次实例化后静态变量是不会重新生成的，除非重新启动程序。
            lockTest testLock = new lockTest();
            testLock.ShowTemp(rtb_CompareText, 1);

            lockTest testLock2 = new lockTest();
            testLock2.ShowTemp(rtb_CompareText, 2);


            lockTest testLock3 = new lockTest();
            testLock3.ShowString(rtb_CompareText, 1);

            lockTest testLock4 = new lockTest();
            testLock4.ShowString(rtb_CompareText, 2);

            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    //Lock锁定的是引用地址，因为LockObjString的引用地址一致，所以不能并发。
                    lock (LockObjString)
                    {
                        rtb_CompareText.AppendText(string.Format("ShowString主线程当前i={0} k={1} ID：{2} 开始 \n", i, k, Thread.CurrentThread.ManagedThreadId));
                        Thread.Sleep(2000);
                        rtb_CompareText.AppendText(string.Format("ShowString主线程当前i={0} k={1} ID：{2} 结束\n", i, k, Thread.CurrentThread.ManagedThreadId));
                    }
                });
            }


            //泛型lock
            //两个int不能并发，因为泛型类在相同参数下，是同一个类。
            //int与string参数不同，所以生成的引用地址也不同。
            lockTestGeneric<int>.ShowGeneric(rtb_CompareText, 1);
            lockTestGeneric<int>.ShowGeneric(rtb_CompareText, 1);
            lockTestGeneric<string>.ShowGeneric(rtb_CompareText, 1);

            new lockTest().ShowThisAnother(rtb_CompareText, 1);
            #endregion

            rtb_CompareText.AppendText(string.Format("btn_ThreadSafety_Click异步方法结束 ID：{0} \n", Thread.CurrentThread.ManagedThreadId));
        }

        private static readonly object obj = new object();

        private readonly string LockObjString = "测试lock字符串";
    }
}
