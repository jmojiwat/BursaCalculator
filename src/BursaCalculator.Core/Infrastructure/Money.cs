using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Money : IEquatable<Money>, IComparable<Money>
    {
        internal readonly decimal Value;

        public Money(decimal value)
        {
            this.Value = value;
        }

        public static Money operator +(Money left, Money right) => new Money(left.Value + right.Value);
        public static Money operator -(Money left, Money right) => new Money(left.Value - right.Value);
        public static decimal operator /(Money left, Money right) => left.Value / right.Value;

        public static Money operator *(Money left, Percent right) => new Money(left.Value * right.Value / 100);
        public static Money operator *(Percent left, Money right) => new Money(left.Value * right.Value / 100);

        public static Money operator *(Money left, Quantity right) => new Money(left.Value * right.Shares);
        public static Money operator *(Quantity left, Money right) => new Money(left.Shares * right.Value);

        public static Money operator *(Money left, Tick right) => new Money(left.Value * (int) right);
        public static Money operator *(Tick left, Money right) => new Money((int) left * right.Value);
        
        public static PerQuantity operator /(Money left, Quantity right) => new PerQuantity(left.Value / right.Shares);
        public static decimal operator /(Money left, int right) => left.Value / right;
        

        public static explicit operator decimal(Money money) => money.Value;

        public bool Equals(Money other) => Math.Abs(Value - other.Value) < 0.005m;

        public override bool Equals(object obj) => obj is Money other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Money left, Money right) => left.Equals(right);

        public static bool operator !=(Money left, Money right) => !left.Equals(right);

        public int CompareTo(Money other) => Value.CompareTo(other.Value);

        public static bool operator <(Money left, Money right) => left.CompareTo(right) < 0;

        public static bool operator >(Money left, Money right) => left.CompareTo(right) > 0;

        public static bool operator <=(Money left, Money right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Money left, Money right) => left.CompareTo(right) >= 0;

        public override string ToString() => $"{Value} MYR";
    }
}