using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Data
{
    public class ProductData
    {
        public readonly double? weight;
        public readonly double? purchasePrice;
        public readonly int packing;
        public readonly double salePrice;

        public int Id { get; }
        public String Title { get; }
        public String Weight 
        {   get 
            {
                if (weight.HasValue)
                    return weight.Value.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture);
                else
                    return "";
            } 
        }
        public String Packing
        {
            get 
            {
                return Measurement.FromInt(packing);
            }
        }
        public String PurchasePrice
        {
            get
            {
                if (purchasePrice.HasValue)
                    return purchasePrice.Value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                else
                    return "";
            }
        }
        public String SalePrice
        {
            get
            {
                return salePrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        public int Number
        {
            get
            {
                return Database.GetProductNumber(Id);
            }
        }

        public ProductData(int id, String title, double? weight, int measurement, double? purchasePrice, double salePrice)
        {
            this.Id = id;
            this.Title = title;
            this.weight = weight;
            this.packing = measurement;
            this.purchasePrice = purchasePrice;
            this.salePrice = salePrice;
        }
    }
}
