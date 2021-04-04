﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model
{
    public class RawProductPresenter
    {
        public readonly Product Product;

        public int Id { get { return Product.Id; } }
        public string Article
        {
            get => Product.Article;
            set => Product.Article = value;
        }
        public string Name
        {
            get => Product.Name;
            set => Product.Name = value;
        }
        public string Category
        {
            get => Product.Category.FullPath;
            set => Product.Category = new ProductCategory(value);
        }
        public int Manufacturer
        {
            get => Product.Manufacturer.Id;
            set => Product.Manufacturer = Model.Manufacturer.Table.Read(value);
        }
        public double PurchasePrice
        {
            get => Product.PurchasePrice.RawValue;
            set => Product.PurchasePrice.RawValue = value;
        }
        public double SalePrice
        {
            get => Product.SalePrice.RawValue;
            set => Product.SalePrice.RawValue = value;
        }
        public double Packing
        {
            get => Product.Measurement.RawValue;
            set => Product.Measurement.RawValue = value;
        }
        public int Measurement
        {
            get => Product.Measurement.Unit.value;
            set => Product.Measurement.Unit = new Unit(value);
        }

        public RawProductPresenter(Product product)
        {
            Product = product;
        }
        public static implicit operator RawProductPresenter(Product productData)
            => new RawProductPresenter(productData);
        public static implicit operator Product(RawProductPresenter product)
            => product.Product;
    }
}
