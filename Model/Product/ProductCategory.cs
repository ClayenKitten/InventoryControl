using System.Collections.Generic;

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
            if (value != Name)
            {
                Parent = new ProductCategory(value.Replace($" / {Name}", string.Empty));
            }
        }
    }
}
