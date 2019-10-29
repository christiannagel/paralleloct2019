using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YieldSample
{
    static class CollectionExtension
    {
        public static IEnumerable<T> Filter<T>(
            this IEnumerable<T> source, 
            Func<T, bool> pred)
        {
            foreach (T item in source)
            {
                if (pred(item))
                {
                    yield return item;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "James", "Jack", "Niki", "Jochen", "Sebastian" };

            var jnames = names.Filter(n => n.StartsWith("J"));

            foreach (var item in jnames)
            {
                Console.WriteLine(item);
            }

            var sd = new SomeData();
            foreach (var item in sd.GetData())
            {
                Console.WriteLine(item);
            }

            IEnumerable<int> enumerable = sd.GetData();
            IEnumerator<int> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int item = enumerator.Current;
                Console.WriteLine(item);
            }
        }
    }

    class SomeData
    {
        public IEnumerable<int> GetData()
        {
            int val = 1;
            var rnd = new Random();

            for (int i = 0; i< 1000; i++)
            {
                Thread.Sleep(rnd.Next(1000));
                yield return val++;
            }
        }
    }
}
