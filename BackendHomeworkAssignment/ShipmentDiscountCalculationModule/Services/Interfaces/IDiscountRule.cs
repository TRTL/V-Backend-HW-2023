using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;

namespace ShipmentDiscountCalculationModule.Services.Interfaces
{
    public interface IDiscountRule
    {
        Shipment GetShipmentPriceAndDiscount(Shipment transaction);
    }
}