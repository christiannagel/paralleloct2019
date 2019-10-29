using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpdateUISample
{
    public class Calculator
    {
        public int Add(int x, int y)
        {
            Thread.Sleep(3000);
            return x + y;
        }
    }
}
