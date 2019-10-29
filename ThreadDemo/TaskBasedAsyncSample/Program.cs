using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasedAsyncSample
{
    class Program
    {
        // async Main method requires C# 7.2
        static async Task Main(string[] args)
        {
            await RunnerAsync();
            Console.ReadLine();
        }

        private static async Task RunnerAsync()
        {
            ShowThreadInfo("runner start");
            Task<string> t1 = GreetingAsync("Stephanie", 3000);
            Task<string> t2 = GreetingAsync("Matthias", 2000);
            await Task.WhenAll(t1, t2);
            ShowThreadInfo("Runner end");
            Console.WriteLine($"{t1.Result}, {t2.Result}");
        }

        static void ShowThreadInfo(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine($"task: {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
        }

        static Task<string> GreetingAsync(string name, int ms)
        {
            return Task<string>.Run(() =>
            {
                return Greeting(name, ms);
            });
        }

        static string Greeting(string name, int ms)
        {
            Thread.Sleep(ms);
            return $"Hello, {name}";
        }
    }


}
