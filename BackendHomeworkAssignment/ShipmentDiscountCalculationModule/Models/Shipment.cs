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

        public Shipment(DateOnly date, EPackageSize packageSizeCode, EProvider carrierCode, double shipmentPrice, double? shipmentDiscount)
        {
            Date = date;
            PackageSizeCode = packageSizeCode;
            CarrierCode = carrierCode;
            ShipmentPrice = shipmentPrice;
            ShipmentDiscount = shipmentDiscount;
        }

        public DateOnly Date { get; set; }
        public EPackageSize PackageSizeCode { get; set; }
        public EProvider CarrierCode { get; set; }
        public double ShipmentPrice { get; set; }
        public double? ShipmentDiscount { get; set; } = null;
    }
}
