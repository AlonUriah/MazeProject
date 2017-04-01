using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Waiting 2 seconds...");
                }
            });
            Task e = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("Waiting 3 seconds...");
                }
            });
            Console.ReadKey();
        }
    }
}
