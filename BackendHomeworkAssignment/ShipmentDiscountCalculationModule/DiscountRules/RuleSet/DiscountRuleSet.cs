using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;

namespace ShipmentDiscountCalculationModule.DiscountRules.RuleSet
{
    static public class DiscountRuleSet
    {
        static public List<IDiscountRule> discountRuleList = new List<IDiscountRule>()
        {
            new AccumulativeMonthlyDiscountFundIsTen(),
            new SmallPackageMatchesLowestPriceAmongProviders(),
            new ThirdLargeLaPostePackageIsFreeOncePerMonth()
        };
    }
}
