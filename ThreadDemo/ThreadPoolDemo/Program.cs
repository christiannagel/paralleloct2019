using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                ThreadPool.GetMinThreads(out int worker, out int completion);
                Console.WriteLine($"min worker: {worker}, compl: {completion}");
            }


            {
                ThreadPool.GetAvailableThreads(out int worker, out int completion);
                Console.WriteLine($"available worker: {worker}, compl: {completion}");

            }

            {
                ThreadPool.GetMaxThreads(out int worker, out int completion);
                Console.WriteLine($"max worker: {worker}, compl: {completion}");

            }
            for (int i = 0; i < 5; i++)
            {
                // ThreadPool.UnsafeQueueNativeOverlapped()
                ThreadPool.QueueUserWorkItem(MyThread);
            }

            Console.ReadLine();

        }

        static void MyThread(object o)
        {
            ShowThreadInfo("MyThread");
        }

        static void ShowThreadInfo(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine($"running in a thread {Thread.CurrentThread.ManagedThreadId}, " +
                $"background: {Thread.CurrentThread.IsBackground}, " +
                $"pooled: {Thread.CurrentThread.IsThreadPoolThread}");

            Console.WriteLine();
        }
    }
}
