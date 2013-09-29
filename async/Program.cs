using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Configuration;
namespace async
{
    public class Program
    {

        private event EventHandler<EventArgs>  someThingChanged;

        enum Choice
        {
            Zero = 0,
            One = 1
        }

        #region AsyncRegion

        public async void Click()
        {
            Console.WriteLine("In Click");

            var result = this.testMethodAsync();
           
            int counter = 0;
            while (!result.IsCompleted)
            {

                Thread.Sleep(1000);
                Console.WriteLine("Doing some shit in Click");
                Console.WriteLine(counter++);
            }

            int value = await result;

            Console.WriteLine("value from job was {0}", value);
            Console.ReadKey();

        }

 

        private async Task<int> testMethodAsync()
        {
            int counter = 0;
            return await Task<int>.Factory.StartNew(() =>
            {
                while (counter++ < 10)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Doing Some shit in testMethodAsync");
                    
                }
                return 10;
            });
        }


        public  void test()
        {
            Console.WriteLine("start in Main");
            this.Click();
            Console.ReadKey();
        
        }



        #endregion
    }
}
