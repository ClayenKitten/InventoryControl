﻿using System;
using System.Globalization;

namespace InventoryControl.Model
{
    public class Money : IMeasurement, IComparable
    {
        private readonly decimal value;
        private readonly Unit unit;
        #region Constructors
        public Money(decimal amount)
        {
            this.value = amount;
            this.unit = Unit.Ruble;
        }
        #endregion

        public override string ToString()
        {
            return this.GetFormattedValue() + this.GetPostfix();
        }
        public string GetRawValue()
        {
            return value.ToString();
        }
        public string GetFormattedValue()
        {
            return value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }
        public string GetPostfix()
        {
            return CultureInfo.GetCultureInfo("ru-RU").NumberFormat.CurrencySymbol;
        }
        public Unit GetUnit()
        {
            return unit;
        }

        public double ToDouble()
        {
            return (double)value;
        }
        public decimal ToDecimal()
        {
            return value;
        }

        public static Money operator +(Money a, Money b)
        {
            return new Money(a.value + b.value);
        }
        public static Money operator -(Money a, Money b)
        {
            return new Money(a.value - b.value);
        }
        public static Money operator *(Money a, int b)
        {
            return new Money(a.value * b);
        }
        public static Money operator /(Money a, int b)
        {
            return new Money(a.value / b);
        }

        public static implicit operator Money(int v)
        {
            return new Money((Decimal)v);
        }
        public static implicit operator Money(double v)
        {
            return new Money((Decimal)v);
        }
        public static implicit operator Money(decimal v)
        {
            return new Money((Decimal)v);
        }
        public static implicit operator Money(long v)
        {
            return new Money((Decimal)v);
        }
        public static implicit operator Money(float v)
        {
            return new Money((Decimal)v);
        }

        public int CompareTo(object obj)
        {
            return value.CompareTo(obj);
        }
    }
}