using System;
using System.Collections.Generic;
using System.Reflection;

namespace InventoryControl.Util
{
    public static class PropertyInfoExtensionMethods
    {
        public static bool HasAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return (propertyInfo.GetCustomAttribute<T>() != null);
        }
    }
}
