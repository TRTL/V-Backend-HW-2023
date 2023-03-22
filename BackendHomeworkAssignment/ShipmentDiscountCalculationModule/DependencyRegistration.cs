using Microsoft.Extensions.DependencyInjection;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using ShipmentDiscountCalculationModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule
{
    public class DependencyRegistration
    {
        public static ServiceProvider Register()
        {
            return new ServiceCollection().AddTransient<IFileReader, FileReader>()
                                          .AddTransient<IFileProcessor, FileProcessor>()
                                          .AddTransient<ITransactionValidator, TransactionValidator>()
                                          .AddTransient<IConsoleService, ConsoleService>()
                                          .AddTransient<IAdapter, Adapter>()
                                          .AddTransient<ICarrierService, CarrierService>()
                                          .AddTransient<IShipmentService, ShipmentService>()
                                          .AddSingleton<IDiscountRuleManager, DiscountRuleManager>()
                                          .BuildServiceProvider();
        }
    }
}
