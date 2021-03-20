﻿using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class ProductMapper
    {
        public static Product Create(Product product)
        {
            const string commandText =
            @"
                INSERT INTO Product(Title, Measurement, Packing, PurchasePrice, SalePrice, Article)
                VALUES($name,$unit,$packing,$purchasePrice,$salePrice,$article);

                SELECT Id FROM Product WHERE ROWID = last_insert_rowid();
            ";
            product.Id = (int)(long)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$name", product.Name),
                new SQLiteParameter("$unit", product.Measurement.GetUnit().value),
                new SQLiteParameter("$packing", product.Measurement.GetRawValue()),
                new SQLiteParameter("$purchasePrice", product.PurchasePrice.GetFormattedValue()),
                new SQLiteParameter("$salePrice", product.SalePrice.GetFormattedValue()),
                new SQLiteParameter("$article", product.Article)
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
                Article=$article
                WHERE Id=$id;
            ";
            Database.CommitNonQueryTransaction(commandText,
                new SQLiteParameter("$name", product.Name),
                new SQLiteParameter("$unit", product.Measurement.GetUnit().value),
                new SQLiteParameter("$packing", product.Measurement.GetRawValue()),
                new SQLiteParameter("$purchasePrice", product.PurchasePrice.GetFormattedValue()),
                new SQLiteParameter("$salePrice", product.SalePrice.GetFormattedValue()),
                new SQLiteParameter("$article", product.Article),
                new SQLiteParameter("$id", product.Id)
            );
        }

        public static Product Read(int id)
        {
            const string commandText = "SELECT * FROM Product WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            if (rdr.Read())
                return new Product
                (
                    id:             rdr.GetInt32(0),
                    name:          rdr.GetString(1),
                    unit:           rdr.GetInt32(2),
                    value:          rdr.GetDouble(3),
                    purchasePrice:  rdr.GetDouble(4),
                    salePrice:      rdr.GetDouble(5),
                    article:        rdr.GetInt32(6)
                );
            else
                throw new KeyNotFoundException();
        }
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

        public static void Delete(int id)
        {
            const string commandText = "DELETE * FROM Product WHERE Id=$id";
            Database.CommitNonQueryTransaction(commandText, new SQLiteParameter("$id", id));
        }
    }
}