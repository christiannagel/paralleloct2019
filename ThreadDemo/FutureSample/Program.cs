using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FutureSample
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Task<int> t1 = new Task<int>(() =>
            {
                return ComplexCalc(3, 5);
            });

            t1.Start();

            Console.WriteLine("in Main");

            int result = t1.Result; // blocking
            Console.WriteLine(result);
        }

        static int ComplexCalc(int x, int y)
        {
            Thread.Sleep(3000);
            return x + y;
        }
    }
}
