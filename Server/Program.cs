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
            Model m = new Model();
            ListCommand ls = new ListCommand(m);
            
            
            Console.ReadKey();
        }
    }
}
