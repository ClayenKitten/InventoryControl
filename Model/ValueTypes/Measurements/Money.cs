using System;
using System.Globalization;

namespace InventoryControl.Model
{
    public class Money : IMeasurement, IComparable
    {
        private decimal rawValue;
        #region Constructors
        public Money(decimal amount)
        {
            this.rawValue = amount;
            Unit = Unit.Ruble;
        }
        #endregion

        public string FormattedValue
            => rawValue.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        public string Postfix
            => CultureInfo.GetCultureInfo("ru-RU").NumberFormat.CurrencySymbol;
        public override string ToString()
            => FormattedValue + Postfix;

        public double RawValue
        {
            get => (double)rawValue;
            set => rawValue = (decimal)value;
        }
        public Unit Unit { get; set; }

        public static Money operator +(Money a, Money b)
            => new Money(a.rawValue + b.rawValue);
        public static Money operator -(Money a, Money b)
            => new Money(a.rawValue - b.rawValue);
        public static Money operator *(Money a, int b)
            => new Money(a.rawValue * b);
        public static Money operator /(Money a, int b)
            => new Money(a.rawValue / b);

        public static implicit operator Money(int v)
            => new Money((Decimal)v);
        public static implicit operator Money(double v)
            => new Money((Decimal)v);
        public static implicit operator Money(decimal v)
            => new Money((Decimal)v);
        public static implicit operator Money(long v)
            => new Money((Decimal)v);
        public static implicit operator Money(float v)
            => new Money((Decimal)v);

        public int CompareTo(object obj)
        {
            return rawValue.CompareTo(obj);
        }
    }
}
