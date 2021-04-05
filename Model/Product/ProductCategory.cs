using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.Model
{
    public class ProductCategory : INamed
    {
        public ProductCategory Parent { get; }
        public string Name { get; set; }

        public string FullPath
            => Parent is null ? Name : Parent.FullPath + "/" + Name;

        public override string ToString()
            => FullPath;

        public ProductCategory(string value)
        {
            Name = value.Split('/')[value.Split('/').Length - 1].Trim();
            if (Name != value)
            {
                var Left = value
                    .Split('/')
                    .Where(x => x.Trim() != Name)
                    .Aggregate((x, y) => $"{x}/{y}")
                    .Trim();
                Parent = new ProductCategory(Left);
            }
        }
    }
}
