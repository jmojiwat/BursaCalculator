using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Money : IEquatable<Money>, IComparable<Money>
    {
        private readonly decimal value;

        public Money(decimal value)
        {
            this.value = value;
        }
        
        public static Money operator +(Money left, Money right) => new Money(left.value + right.value);
        public static Money operator -(Money left, Money right) => new Money(left.value - right.value);
        public static decimal operator /(Money left, Money right) => left.value / right.value;

        public static Money operator *(Money left, Percent right) => new Money(left.value * (decimal) right / 100);
        public static Money operator *(Percent left, Money right) => new Money((decimal) left * right.value / 100);

        public static Money operator *(Money left, Lot right) => new Money(left.value * (int) right * 100);
        public static Money operator *(Lot left, Money right) => new Money((decimal) left * right.value * 100);

        public static Money operator *(Money left, Share right) => new Money(left.value * (int) right);
        public static Money operator *(Share left, Money right) => new Money((decimal) left * right.value);

        public static Money operator *(Money left, Tick right) => new Money(left.value * (int) right);
        public static Money operator *(Tick left, Money right) => new Money((int) left * right.value);
        
        public static Money operator /(Money left, Lot right) => new Money(left.value / ((int) right * 100));
        public static decimal operator /(Money left, int right) => left.value / right;
        

        public static explicit operator decimal(Money money) => money.value;

        public bool Equals(Money other) => Math.Abs(value - other.value) < 0.005m;

        public override bool Equals(object obj) => obj is Money other && Equals(other);

        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(Money left, Money right) => left.Equals(right);

        public static bool operator !=(Money left, Money right) => !left.Equals(right);

        public int CompareTo(Money other) => value.CompareTo(other.value);

        public static bool operator <(Money left, Money right) => left.CompareTo(right) < 0;

        public static bool operator >(Money left, Money right) => left.CompareTo(right) > 0;

        public static bool operator <=(Money left, Money right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Money left, Money right) => left.CompareTo(right) >= 0;

        public override string ToString() => $"{value} MYR";
    }
}