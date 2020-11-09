using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bol1ServicesEjer3
{
    class Program
    {
        public delegate int Operation(int a);

        static readonly private object l = new object(); //declaramos a nuestra llave maestra
        static void Main(string[] args)
        {
            int num = 0;
            Operation op = (a) => a + 1;
            new Thread((a) =>
            {
                Operation op = (a) => a - 1;
                while (num > -1000)
                {
                    lock(l){
                        num = op(num);
                        Console.SetCursorPosition(1, 2);
                        Console.WriteLine("Thread 2:{0}", num);
                    }
                    
                }

            }).Start();
            while (num < 1000)
            {
                lock (l)
                {
                    num = op(num);
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine("Thread 1:{0}", num);
                }
                
            }

            

            Console.ReadLine();
        }

    }
}
