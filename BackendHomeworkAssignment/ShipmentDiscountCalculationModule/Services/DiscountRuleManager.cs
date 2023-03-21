using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;
using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class DiscountRuleManager
    {
        private readonly List<IDiscountRule> _discountRules = new List<IDiscountRule>();

        public void AddRule(IDiscountRule discountRule)
        {
            _discountRules.Add(discountRule);
        }

        public void TryApplyDiscountRules(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            foreach (var discountRule in _discountRules)
            {
                discountRule.TryApplyDiscountRule(shipment, ref remainingMonthlyDiscountFund);
            }
        }
    }
}
