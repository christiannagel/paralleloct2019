using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesWithCollections
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var coll1 = new BlockingCollection<int>();
            Task t1 = WriterTask(coll1);
            Task t2 = ReaderTask(coll1);
            await Task.WhenAll(t1, t2);
            //ConcurrentBag<int> bag = new ConcurrentBag<int>();
            //bag.Add(42);
            //if (bag.TryTake(out int item))
            //{
            //    Console.WriteLine(item);
            //}

            //BlockingCollection<int> coll1 = new BlockingCollection<int>();
            //coll1.Add(42);
            //int x = coll1.Take();


        }

        private static Task ReaderTask(BlockingCollection<int> coll1)
        {
            return Task.Factory.StartNew(async () => 
            {
                await Task.Delay(3000);

                foreach (var item in coll1.GetConsumingEnumerable())
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("all read");
            }, TaskCreationOptions.LongRunning);
        }

        private static Task WriterTask(BlockingCollection<int> coll)
        {
            return Task.Factory.StartNew(async () =>
            {
                var rand = new Random();
                for (int i = 0; i < 100; i++)
                {
                    coll.Add(i);
                    Console.WriteLine($"writer - writing {i}");
                    await Task.Delay(rand.Next(300));
                }
                coll.CompleteAdding();

            }, TaskCreationOptions.LongRunning);
        }
    }
}
