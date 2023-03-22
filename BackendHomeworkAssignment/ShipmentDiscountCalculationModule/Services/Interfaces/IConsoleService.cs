using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services.Interfaces
{
    internal interface IConsoleService
    {
        void PrintLineInvalid(string line);
        void PrintWithHyphenWhereNoDiscount(Shipment shipment);
    }
}
