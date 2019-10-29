using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncPatternsSample
{
    public delegate int ComplexAddDelegate(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            ShowThreadInfo("Main");
            // var d1 = new ComplexAddDelegate(ComplexAdd);
            ComplexAddDelegate d1 = ComplexAdd;
            // int result = d1(42, 2);
            // int result = d1.Invoke(42, 2);
            IAsyncResult ar = d1.BeginInvoke(42, 2, ComplexCalcCompleted, d1);
            // polling via ar.IsCompleted

            var d2 = new Func<int, int, int>(ComplexAdd);
            ar = d2.BeginInvoke(38, 4, ComplexCalcCompleted, d2);

            Console.WriteLine("Main");
            Console.ReadLine();
        }

        static void ComplexCalcCompleted(IAsyncResult ar)
        {
            var d1 = ar.AsyncState as ComplexAddDelegate;
            Func<int, int, int> d2 = ar.AsyncState as Func<int, int, int>;
            int result = 0;
            if (d1 != null)
            {
                result = d1.EndInvoke(ar);
            }
            if (d2 != null)
            {
                result = d2.EndInvoke(ar);
            }
            Console.WriteLine($"das ergebnis ist da {result}");
        }

        static int ComplexAdd(int x, int y)
        {
            ShowThreadInfo("ComplexAdd");
            Thread.Sleep(3000);
            return x + y;
        }

        static object _displayLock = new object();
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
