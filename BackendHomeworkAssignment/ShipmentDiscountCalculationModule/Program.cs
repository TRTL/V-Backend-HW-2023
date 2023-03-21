using Microsoft.Extensions.DependencyInjection;
using ShipmentDiscountCalculationModule.DiscountRules;
using ShipmentDiscountCalculationModule.Services;
using ShipmentDiscountCalculationModule.Services.Interfaces;

namespace ShipmentDiscountCalculationModule
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal remainingMonthlyDiscountFund = 10;

            // Registering dependencies
            var serviceProvider = new ServiceCollection()
            .AddTransient<IFileReader, FileReader>()
            .AddTransient<ITransactionValidator, TransactionValidator>()
            .AddTransient<IAdapter, Adapter>()
            .AddTransient<ICarrierService, CarrierService>()            
            .AddTransient<IShipmentService, ShipmentService>()
            .AddTransient<IConsoleService, ConsoleService>()
            .AddSingleton<DiscountRuleManager>()
            .BuildServiceProvider();

            // Resolving the dependencies
            var fileReader = serviceProvider.GetService<IFileReader>();
            var transactionValidator = serviceProvider.GetService<ITransactionValidator>();
            var adapter = serviceProvider.GetService<IAdapter>();
            var carrierService = serviceProvider.GetService<ICarrierService>();
            var shipmentService = serviceProvider.GetService<IShipmentService>();
            var consoleService = serviceProvider.GetService<IConsoleService>();


            var discountRuleManager = serviceProvider.GetService<DiscountRuleManager>();
            discountRuleManager.AddRule(new AccumulativeMonthlyDiscountFundIsTen());  // Must be first rule → to renew funds when month changes?
            discountRuleManager.AddRule(new SmallPackageMatchesLowestPriceAmongProviders());
            discountRuleManager.AddRule(new ThirdLargeLaPostePackageIsFreeOncePerMonth());


            var fileLines = fileReader.ReadFileLines();
            foreach (var line in fileLines)
            {
                var isTransactionValid = transactionValidator.Validate(line);
                if (!isTransactionValid)
                {
                    Console.WriteLine(line + " Ignored");
                    continue;
                }

                var shipment = adapter.Bind(line);

                carrierService.GetCarrierPrice(shipment);

                discountRuleManager.TryApplyDiscountRules(shipment, ref remainingMonthlyDiscountFund);

                shipmentService.ApplyDiscountToPrice(shipment);

                consoleService.PrintWithHyphenWhereNoDiscount(shipment);
            }
        }
    }
}