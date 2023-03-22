using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;
using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services.Interfaces
{
    internal interface IDiscountRuleManager
    {
        void AddRules(List<IDiscountRule> discountRuleList);
        void TryApplyDiscountRules(Shipment shipment, ref decimal remainingMonthlyDiscountFund);
    }
}
