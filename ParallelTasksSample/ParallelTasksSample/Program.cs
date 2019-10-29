using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasksSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(DoFoo, DoBar, DoFoo);

            Task t1 = new Task(DoBar);
            t1.Start();

            Task t2 = Task.Run(DoFoo);

            Task t3 = Task.Factory.StartNew(DoBar);

            var tf = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
            tf.StartNew(DoFooBar);

            Console.WriteLine("Main ended");

            Task.WaitAll(t1, t2, t3);
//            Console.ReadLine();
        }

        static void DoFoo()
        {
            ShowThreadInfo("Foo");
        }

        static void DoBar()
        {
            ShowThreadInfo("Bar");
        }

        static void DoFooBar()
        {
            ShowThreadInfo("FooBar");
        }

        private static object _displayLock = new object();
        static void ShowThreadInfo(string title)
        {
            lock (_displayLock)
            {
                Console.WriteLine(title);
                Console.WriteLine($"running in a thread {Thread.CurrentThread.ManagedThreadId}, " +
                    $"background: {Thread.CurrentThread.IsBackground}, " +
                    $"pooled: {Thread.CurrentThread.IsThreadPoolThread}");
                Console.WriteLine($"task: {Task.CurrentId}");

                Console.WriteLine();
            }
        }
    }
}
