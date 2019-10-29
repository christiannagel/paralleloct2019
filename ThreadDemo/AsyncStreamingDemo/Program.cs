using System;
using System.Threading.Tasks;

namespace AsyncStreamingDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunnerAsync();
            Console.ReadLine();
        }

        private static async Task RunnerAsync()
        {
            var dev = new ADevice();
            await foreach(var d in dev.GetSensor1Data())
            {
                Console.WriteLine(d);
            }
        }
    }
}
