using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class FileProcessor : IFileProcessor
    {
        private ITransactionValidator _validator;
        private IConsoleService _console;
        private IAdapter _adapter;
        private ICarrierService _carrier;
        private IDiscountRuleManager _discountRule;
        private IShipmentService _shipment;

        public FileProcessor(ITransactionValidator transactionValidator,
                             IConsoleService consoleService, 
                             IAdapter adapter,
                             ICarrierService carrierService,
                             IDiscountRuleManager discountRuleManager,
                             IShipmentService shipmentService)
        {
            _validator = transactionValidator;
            _console = consoleService;
            _adapter = adapter;
            _carrier = carrierService;
            _discountRule = discountRuleManager;
            _shipment = shipmentService;
        }

        public void Process(string[] fileLines, ref decimal remainingMonthlyDiscountFund)
        {


            foreach (var line in fileLines)
            {
                var isTransactionValid = _validator.Validate(line);
                if (!isTransactionValid)
                {
                    _console.PrintLineInvalid(line);
                    continue;
                }

                var shipment = _adapter.Bind(line);

                _carrier.GetCarrierPrice(shipment);

                _discountRule.TryApplyDiscountRules(shipment, ref remainingMonthlyDiscountFund);

                _shipment.ApplyDiscountToPrice(shipment);

                _console.PrintWithHyphenWhereNoDiscount(shipment);
            }
        }
    }
}
