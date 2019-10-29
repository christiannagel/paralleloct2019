using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasedAsyncPatternWithExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner();
            Console.ReadLine();
        }

        private static async void Runner()
        {
            Task retTask = null;
            try
            {
                List<Task> tasks = new List<Task>();
                Task t1 = ThrowAfterAsync("first", 3000);
                tasks.Add(t1);
                Task t2 = ThrowAfterAsync("second", 2000);
                tasks.Add(t2);
                retTask = Task.WhenAll(tasks.ToArray());
                await retTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"in runner {ex.Message}");

                var exceptions = retTask.Exception;
                foreach (var innerEx in exceptions.InnerExceptions)
                {
                    Console.WriteLine(innerEx.Message);
                }
            }
            Console.WriteLine("Runner completed");
        }

        static Task ThrowAfterAsync(string message, int ms)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(ms);
                throw new Exception(message);
            });
        }
    }
}
