using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BufferWidth = Console.WindowWidth = 50;
            Console.BufferHeight = Console.WindowHeight = 50;
            int x = 20;
            int y = 20;
            int a = 10;
            int b = 10;
            En test = new En();
            En secondTEst = new En();
            while (true)
            {
                test.Image(x, y, 1);
                Thread.Sleep(200);
                Console.Clear();
            }
        }
    }
    //Direction (under construction)
}


