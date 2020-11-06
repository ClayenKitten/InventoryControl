using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl
{
    public class ProductData
    {
        private readonly double? weight;
        private readonly double? purchasePrice;
        private readonly double? salePrice;

        public int Id { get; }
        public String Title { get; }
        public int Number { get; }
        public String Weight 
        {   get 
            {
                if (weight.HasValue)
                    return weight.Value.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture);
                else
                    return "";
            } 
        }
        public String Measurement { get; }
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
                if (salePrice.HasValue)
                    return salePrice.Value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                else
                    return "";
            }
        }


        public ProductData(int id, String title, int number, double? weight, String measurement, double? purchasePrice, double? salePrice)
        {
            this.Id = id;
            this.Title = title;
            this.Number = number;
            this.weight = weight;
            this.Measurement = measurement.ToString();
            this.purchasePrice = purchasePrice;
            this.salePrice = salePrice;
        }
        public string getSaleSum(int number)
        {
            if (salePrice.HasValue)
                return (salePrice.Value * number).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            else
                return "";
        }
    }
    public class Measurement
    {
        public static readonly String Kilogram = "кг";
        public static readonly String Piece = "шт";
        public static String FromInt(int value)
        {
            switch (value)
            {
                case 0:
                    return Kilogram;
                case 1:
                    return Piece;
                default:
                    return "WRONG MEASUREMENT NUMBER";
            }
        }
    }
}
