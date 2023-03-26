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
            // Registering dependencies
            var serviceProvider = DependencyRegistration.Register();

            // Resolving the dependencies
            var discountRuleManager = serviceProvider.GetService<IDiscountRuleManager>();
            var fileReader = serviceProvider.GetService<IFileReader>();
            var fileProcessor = serviceProvider.GetService<IFileProcessor>();

            var ruleList = DiscountRuleSet.discountRuleList;
            discountRuleManager.AddRules(ruleList);
            var fileLines = fileReader.ReadFileLines();
            fileProcessor.Process(fileLines);
        }
    }
}