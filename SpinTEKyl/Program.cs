using System;
using System.Linq;

namespace SpinTEKyl
{
    class Program
    {
        
        private static string _basePath = null!;

        static void Main(string[] args)
        {

            string userInput = "";
            bool validInput = false;
            
            _basePath = args.Length == 1 ? args[0] : System.IO.Directory.GetCurrentDirectory();

            while (!validInput)
            {
                Console.WriteLine();
                Console.WriteLine("Enter year number between 1900 and 2100: ");
                var input = Console.ReadLine()?.Trim();

                if (input != null && input.All(char.IsNumber)
                                  && int.Parse(input) >= 1900 && int.Parse(input) <= 2100)
                {
                    validInput = true;
                    userInput = input;
                }
            }

            DateHelper dateHelperInstance = new DateHelper(userInput);

            Console.WriteLine();

            PaymentTableHelper paymentTableHelper = new PaymentTableHelper(dateHelperInstance);

            var csvStringToWrite = paymentTableHelper.CreatePaymentTableCsvString();
            paymentTableHelper.WritePaymentTableCsvFile(csvStringToWrite, _basePath);
        }
    }
}
