using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class Adapter : IAdapter
    {
        public Shipment Bind(string inputLine)
        {
            var shipment = new Shipment();

            var inputLineSplit = inputLine.Split(' ');

            var dateFromString = inputLineSplit[0];
            shipment.Date = DateOnly.Parse(dateFromString);

            var packageSizeCodeFromString = inputLineSplit[1];
            shipment.PackageSizeCode = Enum.Parse<EPackageSize>(packageSizeCodeFromString);

            var carrierCodeFromString = inputLineSplit[2];
            shipment.CarrierCode = Enum.Parse<EProvider>(carrierCodeFromString);

            return shipment;
        }
    }
}
