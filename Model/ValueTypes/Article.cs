using System;
using System.Text.RegularExpressions;

namespace InventoryControl.Model
{
    public class Article
    {
        public string EAN13 { get; set; }
        public string Addition { get; set; }
        public override string ToString()
        {
            if(Addition != string.Empty)
            {
                return $"{EAN13}-{Addition}";
            }
            else
            {
                return $"{EAN13}";
            }
        }
        public static bool CanParse(string value)
            => Regex.IsMatch(value, "^([0-9]{13})+((-)+([0-9]{1,})){0,1}$");
        public Article(string value)
        {
            if (CanParse(value))
            {
                if(value.Contains("-"))
                {
                    EAN13 = value.Split('-')[0];
                    Addition = value.Split('-')[1];
                }
                else
                {
                    EAN13 = value;
                    Addition = string.Empty;
                }
            }
            else
            {
                throw new ArgumentException("Invalid article number provided", "value");
            }
        }
    }
}
