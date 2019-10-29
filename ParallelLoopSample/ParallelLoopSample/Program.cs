using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoopSample
{
    class Program
    {
        static void Main(string[] args)
        {

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.Register(() =>
            {
                Console.WriteLine("Main - cancellation requested!!!");
            });
            cts.CancelAfter(500);

            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine(i);
            //}

            try
            {
                Parallel.For(0, 1000, new ParallelOptions() { CancellationToken = cts.Token }, MyAction);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var innerex in ex.InnerExceptions)
                {
                    Console.WriteLine(innerex.Message);
                }
            }
            //Parallel.For(0, 1000, x =>
            //{
            //    Console.WriteLine(x);
            //    ShowThreadInfo($"thread for x: {x}");
            //});

            //IEnumerable<SomeData> someData = Enumerable.Range(0, 1000)
            //    .Select(x => new SomeData { Number = x, Text = $"data {x}" }).ToArray();

            //someData.AsParallel().Where(md => md.Number > 500).ForAll(md =>
            //{
            //    Console.WriteLine(md);
            //});



            //Parallel.ForEach(someData, sd =>
            //{
            //    ShowThreadInfo(sd.ToString());
            //});
            Console.WriteLine("Main end");
        }

        static void MyAction(int x, ParallelLoopState loopState)
        {
            // Console.WriteLine(x);
            Console.WriteLine($"myaction start {x}");
            ShowThreadInfo($"thread for x: {x}");
            for (int i = 0; i < 10; i++)
            {
                if (loopState.ShouldExitCurrentIteration)
                {
                    Console.WriteLine("stepping out...");
                    throw new OperationCanceledException();
                }
                Thread.Sleep(5);
                
            }
            Console.WriteLine($"myaction end {x}");
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
