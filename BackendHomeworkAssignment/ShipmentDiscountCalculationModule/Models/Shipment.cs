using ShipmentDiscountCalculationModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Models
{
    public class Shipment
    {
        public Shipment() { }

        public Shipment(DateOnly date, EPackageSize packageSizeCode, EProvider carrierCode)
        {
            Date = date;
            PackageSizeCode = packageSizeCode;
            CarrierCode = carrierCode;
        }

        public DateOnly Date { get; set; }
        public EPackageSize PackageSizeCode { get; set; }
        public EProvider CarrierCode { get; set; }
        public decimal? ShipmentPrice { get; set; }
        public decimal? ShipmentDiscount { get; set; } = null;
    }
}
