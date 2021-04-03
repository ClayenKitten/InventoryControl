using System.Text.RegularExpressions;

namespace InventoryControl.Util
{
    public class StringValidator
    {
        public bool Validate(string value, out string error)
        {
            if (value.Trim() == string.Empty)
            {
                if (IsRequired)
                {
                    error = "Поле обязательно для заполнения";
                    return false;
                }
                else
                {
                    error = "";
                    return true;
                }
            }
            switch (Type)
            {
                case ValidationEnum.None:
                    error = "";
                    return true;
                case ValidationEnum.Integer:
                    error = "Ожидается целое число";
                    return new Regex(@"^[+-]?([1-9]|[0](?![0-9]))([0-9])*$").IsMatch(value);
                case ValidationEnum.Real:
                    error = "Ожидается вещественное число";
                    return new Regex(@"^[+-]?(([1-9]\d*\.?\d*$)|(0(\.\d*)?$)|(\.\d+)$)").IsMatch(value);
                case ValidationEnum.Money:
                    error = "Ожидается денежная сумма";
                    return new Regex(@"^(([1-9]\d*(\.\d{0,2})?$)|(0(\.\d{0,2})?$))").IsMatch(value);
                default:
                    error = "Ошибка валидации";
                    return false;
            }
        }
        public bool Validate(string value)
            => Validate(value, out _);

        public bool IsRequired { get; set; }
        public ValidationEnum Type { get; set; }

        public StringValidator(bool isRequired, ValidationEnum type)
        {
            IsRequired = isRequired;
            Type = type;
        }
    }
}
