using ShipmentDiscountCalculationModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Models
{
    public class ProviderShippingPrice
    {
        public EProvider Provider { get; set; }
        public EPackageSize PackageSize { get; set; }
        public double Price { get; set; }
    }
}
