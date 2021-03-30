using InventoryControl.ORM;
using InventoryControl.Util;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class ProductMapper
    {
        public static Product Create(Product product)
            => Product.Table.Create(product);

        public static void Update(Product product)
        {
            const string commandText =
            @"
                UPDATE Product SET

                Title=$name,
                Measurement=$unit,
                Packing=$packing,
                PurchasePrice=$purchasePrice,
                SalePrice=$salePrice,
                Article=$article,
                ManufacturerId=$manufacturerId,
                Category=$category
                WHERE Id=$id;
            ";
            Database.CommitNonQueryTransaction(commandText,
                new SQLiteParameter("$name", product.Name),
                new SQLiteParameter("$unit", product.Measurement.GetUnit().value),
                new SQLiteParameter("$packing", product.Measurement.GetRawValue()),
                new SQLiteParameter("$purchasePrice", product.PurchasePrice.GetFormattedValue()),
                new SQLiteParameter("$salePrice", product.SalePrice.GetFormattedValue()),
                new SQLiteParameter("$article", product.Article),
                new SQLiteParameter("$manufacturerId", product.Manufacturer.Id),
                new SQLiteParameter("$category", product.Category.FullPath),
                new SQLiteParameter("$id", product.Id)
            );
        }

        public static Product Read(int id)
            => Product.Table.Read(id);
        public static List<Product> GetFullDictionary()
            => (List<Product>)Product.Table.ReadAll();
    }
}
