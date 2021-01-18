using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    class RealValidationRule : ValidationRule
    {
        public bool NotNull { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (NotNull && value as string == string.Empty)
            {
                return new ValidationResult(false, "Значение обязательно для ввода");
            }
            bool canConvert = double.TryParse(value as string, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
            return new ValidationResult(canConvert, "Ожидается число");
        }
    }
}
