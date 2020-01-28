using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0, b = 0;
            int precision = 0;
            string operation;
            Operation type = Operation.Add;

            try
            {
                Console.Write("The first number: ");
                a = double.Parse(Console.ReadLine());

                Console.Write("The second number: ");
                b = double.Parse(Console.ReadLine());

                Console.Write("Precision: ");
                precision = int.Parse(Console.ReadLine());

                Console.Write("Operation(Div, Sub, Add, Mul): ");
                operation = Console.ReadLine();

                switch (operation)
                {
                    case "Div":
                        type = Operation.Divide;
                        break;
                    case "Sub":
                        type = Operation.Substract;
                        break;
                    case "Add":
                        type = Operation.Add;
                        break;
                    case "Mul":
                        type = Operation.Multiply;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown operation");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!\n" + ex.Message);
            }
            finally
            {
                Console.WriteLine();
            }

            Calculator calc = new Calculator(a, b, type, precision);
            calc.Calculate();

            Console.ReadKey();
        }
    }
}
