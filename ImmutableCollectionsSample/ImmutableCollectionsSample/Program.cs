using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableCollectionsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };

            ImmutableList<int> list2 = list1.ToImmutableList();
            _ = ShowListAsync(list2);
            list2 = list2.Add(42);
            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }



            string s1 = "hello";
            s1 = "hello, world";
            Console.ReadLine();
        }


        private static async Task ShowListAsync(ImmutableList<int> list)
        {
            await Task.Delay(100);
            foreach (var item in list)
            {
                Console.WriteLine($"showlistasync {item}");
            }
        }
    }
}
