using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {

        /// <summary>
        /// await/async C#5.0 .NetFramework4.5及以上支持，
        /// 本身不产生线程，但是依托于Task而存在，所以程序执行时也是多线程。
        /// 
        /// async可以随便添加，可以不是用await,
        /// await只能出现在Task前面且必须声明async。
        /// 
        /// 
        /// 加了await后，将后面代码操作包装成了回调，
        /// 这个回调由await之前的Task执行(例如用例ReturnTask方法)。
        /// ******类似于接力***********
        /// 主线程调用后，Task产生子线程 第一个await直接返回主线程，
        /// 然后用子线程执行下面的操作，遇到第二个await直接返回第一个的子线程，
        /// 产生第二个await的Task子线程执行下面操作。
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Main方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);


            new AwaitAsyncClass().ShowTask();

            Console.WriteLine("Main方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
        }
    }


    public class AwaitAsyncClass
    {

        public void ShowTask()
        {
            //NoReturn();

            ReturnTask();

            //ReturnLong();

            //ReturnTaskLong();
        }
        public void NoReturn()
        {
            //主线程（调用线程），
            Console.WriteLine("NoReturn方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);

            //主线程发起，启动新线程执行
            Task.Run(() =>
             {
                 //Task子线程完成下面的操作
                 Console.WriteLine("NoReturn-Task方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                 Thread.Sleep(1000);
                 Console.WriteLine("NoReturn-Task方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);
             });

            //Task子线程完成下面打印，
            //如果没有await，应该由主线程完成打印。
            Console.WriteLine("ReturnTask方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);
        }

        public async Task ReturnTask()
        {
            //主线程（调用线程），
            Console.WriteLine("ReturnTask方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);

            //主线程发起，启动新线程执行
            var task = Task.Run(() =>
            {
                //Task子线程完成下面的操作
                Console.WriteLine("ReturnTask-Task方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
                Console.WriteLine("ReturnTask-Task方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);
            });
            //主线程返回执行自己的操作。
            await task;
            //Task子线程完成下面打印，
            //如果没有await，应该由主线程完成打印。
            Console.WriteLine("ReturnTask方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);


            //主线程发起，启动新线程执行
            await Task.Run(() =>
             {
                 //Task子线程完成下面的操作
                 Console.WriteLine("ReturnTask2-Task方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                 Thread.Sleep(1000);
                 Console.WriteLine("ReturnTask2-Task方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);
             });
        }

        public long ReturnLong()
        {
            //主线程（调用线程），
            Console.WriteLine("ReturnLong方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
            long result = 0;
            //主线程发起，启动新线程执行
            Task.Run(() =>
            {
                //Task子线程完成下面的操作
                Console.WriteLine("ReturnLong-Task方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                for (int i = 0; i < 100000; i++)
                {
                    result += i;
                }
                Console.WriteLine("ReturnLong-Task方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                return result;
            });

            Console.WriteLine("ReturnLong方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);

            return result;
        }

        public async Task<long> ReturnTaskLong()
        {
            //主线程（调用线程），
            Console.WriteLine("ReturnTaskLong方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
            long result = 0;
            //主线程发起，启动新线程执行
            await Task.Run(() =>
            {
                //Task子线程完成下面的操作
                Console.WriteLine("ReturnTaskLong-Task方法开始，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                for (int i = 0; i < 100000; i++)
                {
                    result += i;
                }
                Console.WriteLine("ReturnTaskLong-Task方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);
                return result;
            });

            Console.WriteLine("ReturnTaskLong方法结束，ID：{0}", Thread.CurrentThread.ManagedThreadId);

            return result;
        }

    }
}
