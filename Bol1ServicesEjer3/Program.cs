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
        public static bool flag = true;

        static readonly private object l = new object(); //declaramos a nuestra llave maestra
        static void Main(string[] args)
        {
            int num = 0;
            Operation op;
            Thread thread1 = new Thread(() =>
            {
                Operation op = (a) => a - 1;
                while (flag)
                {
                    lock (l)
                    {
                        if (flag)
                        {
                            num = op(num);
                            Console.WriteLine("Thread 2:{0}", num);
                            if (num == -100)
                            {
                                flag = false;
                            }
                        }

                    }

                }

            });

            Thread thread2 = new Thread(() =>
            {
                Operation op = (a) => a + 1;
                while ( flag)
                {
                    lock (l)
                    {
                        if (flag)
                        {
                            num = op(num);
                            Console.WriteLine("Thread 1:{0}", num);
                            if (num == 100)
                            {
                                flag = false;
                            }
                        }

                    }

                }

            });
            thread1.Start();
            thread2.Start();

            thread1.Join(); // podemos apañar con uno ya que se paran los dos si uno gana

            if (num > 0) Console.WriteLine("Gana hilo 1");
            else Console.WriteLine("Gana hilo 2");
            Console.ReadLine();
            
            
        }

    }
}


//while (num < 1000 && flag)
//{
//    lock (l)
//    {
//        if (flag)
//        {
//            num = op(num);
//            Console.WriteLine("Thread 1:{0}", num);
//            if (num == 1000)
//            {
//                flag = false;
//            }
//        }

//    }

//}