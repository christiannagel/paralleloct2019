using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ShowThreadInfo("Main");
            Thread t1 = new Thread(MyThread);
            t1.SetApartmentState(ApartmentState.STA);  // important for COM
            t1.IsBackground = true;
            t1.Start();
            Console.WriteLine("Main end");
        }

        static void MyThread()
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
