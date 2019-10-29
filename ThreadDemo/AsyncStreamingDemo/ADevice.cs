using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStreamingDemo
{
    public class ADevice
    {

        public async IAsyncEnumerable<int> GetSensor1Data()
        {
            int val = 1;
            var rnd = new Random();

            for (int i = 0; i < 1000; i++)
            {
                await Task.Delay(rnd.Next(1000));
                yield return val++;
            }

        }
    }
}
