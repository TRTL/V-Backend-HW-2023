using Microsoft.Extensions.DependencyInjection;
using ShipmentDiscountCalculationModule.DiscountRules;
using ShipmentDiscountCalculationModule.DiscountRules.RuleSet;
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
            .AddTransient<IFileProcessor, FileProcessor>()
            .AddTransient<ITransactionValidator, TransactionValidator>()
            .AddTransient<IConsoleService, ConsoleService>()
            .AddTransient<IAdapter, Adapter>()
            .AddTransient<ICarrierService, CarrierService>()            
            .AddTransient<IShipmentService, ShipmentService>()
            .AddSingleton<IDiscountRuleManager,DiscountRuleManager>()
            .BuildServiceProvider();

            // Resolving the dependencies
            var fileReader = serviceProvider.GetService<IFileReader>();
            var fileProcessor = serviceProvider.GetService<IFileProcessor>();

            var discountRuleManager = serviceProvider.GetService<IDiscountRuleManager>();
            discountRuleManager.AddRules(DiscountRuleSet.discountRuleList);

            var fileLines = fileReader.ReadFileLines();
            fileProcessor.Process(fileLines, ref remainingMonthlyDiscountFund);
        }
    }
}