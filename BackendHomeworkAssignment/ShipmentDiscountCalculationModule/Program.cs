using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services;
using ShipmentDiscountCalculationModule.Services.Interfaces;

namespace ShipmentDiscountCalculationModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileReader fileReader = new FileReader("\\Data\\input.txt");
            var fileLines = fileReader.ReadFileLines();

            foreach (var line in fileLines)
            {
                IValidator validator = new Validator();
                var isShipmentValid = validator.Validate(line);
                if (!isShipmentValid)
                {
                    Console.WriteLine(line + " Ignored");
                    continue;
                }


                Console.WriteLine(line);
            }





        }
    }
}