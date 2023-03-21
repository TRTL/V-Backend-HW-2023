using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class ConsoleService : IConsoleService
    {
        public void PrintWithHyphenWhereNoDiscount(Shipment shipment)
        {
            string discount = shipment.ShipmentDiscount == null ? "-" : shipment.ShipmentDiscount.Value.ToString();
            Console.WriteLine($"{shipment.Date} {shipment.PackageSizeCode} {shipment.CarrierCode} {shipment.ShipmentPrice} {discount} ");
        }
    }
}
