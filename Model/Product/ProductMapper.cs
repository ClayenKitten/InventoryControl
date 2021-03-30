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
            => Product.Table.Update(product);
        public static Product Read(int id)
            => Product.Table.Read(id);
        public static List<Product> GetFullDictionary()
            => (List<Product>)Product.Table.ReadAll();
    }
}
