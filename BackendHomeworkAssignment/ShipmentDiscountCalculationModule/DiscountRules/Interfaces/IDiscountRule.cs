using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;

namespace ShipmentDiscountCalculationModule.DiscountRules.Interfaces
{
    public interface IDiscountRule
    {

        void TryApplyDiscountRule(Shipment shipment, ref decimal remainingMonthlyDiscountFund);
    }
}