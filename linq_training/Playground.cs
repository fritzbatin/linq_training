using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_training
{
    class Playground
    {
        public static void myPlayground()
        {
            MyObject myObject = new MyObject();
            Console.WriteLine(myObject.MyMethod());

            Console.WriteLine("default myObject value and processed in num3 is {0}", myObject.num3);

            Console.WriteLine();
            myObject.number1 = 5;
            myObject.number2 = 10;
            Console.WriteLine($"The Calculate() value of number1 + number2 is {myObject.Calculate()}");
            Console.WriteLine($"The Calculate1() value of number1 + number2 is {myObject.Calculate1(5,10)}");

            myObject.num1 = 5;
            myObject.num2 = 10;

            Console.WriteLine($"num3 property value is {myObject.num3}");

            MyObject myObject1 = new MyObject(10,10);
            Console.WriteLine($"myObject1 property value is {myObject1.num3}");



        }
        class MyObject
        {
            int _number1 = 0;
            int _number2 = 0;
            public MyObject()
            {
                _number1 = 5;
                _number2 = 7;
            }
            public MyObject(int num1, int num2)
            {
                _number1 = num1;
                _number2 = num2;
            }

            /// <summary>
            /// 
            /// </summary>
            public int number1 { get; set; }
            public int number2 { get; set; }
            public int Calculate() {
                return number1 + number2;
            }

            public  int Calculate1(int num1, int num2)
            {
                return num1 + num2;
            }
            

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// 
          
            public int num1
            {
                get
                {
                    return _number1;
                }
                set
                {
                    _number1 = value;
                }
            }
            public int num2
            {
                get
                {
                    return _number2;
                }
                set
                {
                    _number2 = value;
                }
            }

            public int num3
            {
                get
                {
                    return _number1 + _number2;
                }
            }
/// <summary>
/// 
/// </summary>
/// <returns></returns>
            public string MyMethod() {
                return "return value from this method";
            }
        }
    }
}
