using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    class IntegerValidationRule : ValidationRule
    {
        public bool NotNull { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (NotNull && value as string == string.Empty)
            {
                return new ValidationResult(false, "Значение обязательно для ввода");
            }
            bool canConvert = int.TryParse(value as string, out _);
            return new ValidationResult(canConvert, "Ожидается целое число");
        }
    }
}
