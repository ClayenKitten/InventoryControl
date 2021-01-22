﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Product
{
    public static class ProductDataMapper
    {
        public static int Create()
        {
            const string commandText =
            @"
                INSERT INTO ProductsDictionary DEFAULT VALUES; 
                SELECT Id FROM ProductsDictionary 
                WHERE ROWID = last_insert_rowid();
            ";
            return (int)Database.CommitScalarTransaction(commandText);
        }
        public static int Create(ProductData product)
        {
            var id = ProductDataMapper.Create();
            ProductDataMapper.Update(id, product);
            return id;
        }

        public static void Update(int id, ProductData product)
        {
            const string commandText =
            @"
                UPDATE ProductsDictionary SET

                Title=$name,
                Measurement=$unit,
                Packing=$packing,
                PurchasePrice=$purchasePrice,
                SalePrice=$salePrice
                WHERE Id=$id;
            ";
            Database.CommitNonQueryTransaction(commandText,
                new SQLiteParameter("$name", product.Name),
                new SQLiteParameter("$unit", product.Measurement.GetUnit().value),
                new SQLiteParameter("$packing", product.Measurement.GetRawValue()),
                new SQLiteParameter("$purchasePrice", product.PurchasePrice),
                new SQLiteParameter("$salePrice", product.SalePrice),
                new SQLiteParameter("$id", id)
            );
        }
        
        public static ProductData Read(int id)
        {
            const string commandText = "SELECT * FROM Product WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            if (rdr.Read())
                return new ProductData
                (
                    id:             rdr.GetInt32(0),
                    title:          rdr.GetString(1),
                    purchasePrice:  0.5,//rdr.GetDouble(4),
                    salePrice:      1.2,//rdr.GetDouble(5),
                    unit:           rdr.GetInt32(2),
                    value:          rdr.GetDouble(3)
                );
            else
                throw new KeyNotFoundException();
        }
        
        public static void Delete(int id)
        {
            const string commandText = "DELETE * FROM ProductsDictionary WHERE Id=$id";
            Database.CommitNonQueryTransaction(commandText, new SQLiteParameter("$id", id));
        }
    }
}
