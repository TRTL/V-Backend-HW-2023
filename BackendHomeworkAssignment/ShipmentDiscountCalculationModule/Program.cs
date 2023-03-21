using Microsoft.Extensions.DependencyInjection;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ShipmentDiscountCalculationModule
{
    class Program
    {
        static void Main(string[] args)
        {
            // Registering dependencies
            var serviceProvider = new ServiceCollection()
            .AddTransient<IFileReader, FileReader>()
            .AddTransient<ITransactionValidator, TransactionValidator>()
            .AddTransient<IAdapter, Adapter>()
            .AddSingleton<IDiscountRule, DiscountRule>()
            .BuildServiceProvider();

            // Resolving the dependencies
            var fileReader = serviceProvider.GetService<IFileReader>();
            var transactionValidator = serviceProvider.GetService<ITransactionValidator>();
            var adapter = serviceProvider.GetService<IAdapter>();
            var discountRule = serviceProvider.GetService<IDiscountRule>();


            string[] fileLines = fileReader.ReadFileLines();

            foreach (var line in fileLines)
            {
                var isTransactionValid = transactionValidator.Validate(line);
                if (!isTransactionValid)
                {
                    Console.WriteLine(line + " Ignored");
                    continue;
                }

                Shipment transaction = adapter.Bind(line);
                Shipment shipment = discountRule.GetShipmentPriceAndDiscount(transaction);

                string discount = shipment.ShipmentDiscount == null ? "-" : shipment.ShipmentDiscount.Value.ToString();
                Console.WriteLine($"{shipment.Date} {shipment.PackageSizeCode} {shipment.CarrierCode} {shipment.ShipmentPrice} {discount} ");
            }
        }
    }
}