using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrderPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.GetValue(0).ToString();
            
            var inputReader = new InputProcessor();            

            inputReader.ProcessInput(input);

            var printValues = inputReader.PrintOutput();

            Console.WriteLine(printValues);


        }
    }
}
