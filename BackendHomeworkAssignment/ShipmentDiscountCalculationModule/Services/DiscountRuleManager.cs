using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class DiscountRuleManager : IDiscountRuleManager
    {
        private readonly List<IDiscountRule> _discountRules = new List<IDiscountRule>();

        public void AddRules(List<IDiscountRule> discountRuleList)
        {
            foreach (var rule in discountRuleList)
            {
                _discountRules.Add(rule);
            }
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
