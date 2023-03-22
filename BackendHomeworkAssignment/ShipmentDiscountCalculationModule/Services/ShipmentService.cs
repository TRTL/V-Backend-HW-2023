using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    public class ShipmentService : IShipmentService
    {
        public void ApplyDiscountToPrice(Shipment shipment)
        {
            if (shipment.ShipmentDiscount != null) shipment.ShipmentPrice -= shipment.ShipmentDiscount;
        }
    }
}
