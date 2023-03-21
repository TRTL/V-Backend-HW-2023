using ShipmentDiscountCalculationModule.Data;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class CarrierService : ICarrierService
    {
        public void GetCarrierPrice(Shipment shipment)
        {
            shipment.ShipmentPrice = ProviderPriceData.priceList.Find(p => p.PackageSize == shipment.PackageSizeCode && p.Provider == shipment.CarrierCode).Price;
        }
    }
}
