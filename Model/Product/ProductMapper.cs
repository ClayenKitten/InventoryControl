using InventoryControl.ORM;
using InventoryControl.Util;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class ProductMapper
    {
        public static Product Create(Product product)
        {
            const string commandText =
            @"
                INSERT INTO Product(Title, Measurement, Packing, PurchasePrice, SalePrice, Article, ManufacturerId, Category)
                VALUES($name,$unit,$packing,$purchasePrice,$salePrice,$article,$manufacturerId,$category);

                SELECT Id FROM Product WHERE ROWID = last_insert_rowid();
            ";
            product.Id = (int)(long)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$name", product.Name),
                new SQLiteParameter("$unit", product.Measurement.GetUnit().value),
                new SQLiteParameter("$packing", product.Measurement.GetRawValue()),
                new SQLiteParameter("$purchasePrice", product.PurchasePrice.GetFormattedValue()),
                new SQLiteParameter("$salePrice", product.SalePrice.GetFormattedValue()),
                new SQLiteParameter("$article", product.Article),
                new SQLiteParameter("$manufacturerId", product.Manufacturer.Id),
                new SQLiteParameter("$category", product.Category.FullPath)
            );
            return product;
        }

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
        {
            var dictionary = new List<Product>();
            const string commandText = "SELECT Product.Id FROM Product";
            using var rdr = Database.CommitReaderTransaction(commandText);
            while (rdr.Read())
            {
                dictionary.Add(ProductMapper.Read(rdr.GetInt32(0)));
            }
            return dictionary;
        }
    }
}
