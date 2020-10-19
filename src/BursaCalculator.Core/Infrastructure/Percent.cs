using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Percent : IEquatable<Percent>, IComparable<Percent>
    {
        private readonly decimal value;

        public Percent(decimal value)
        {
            this.value = value;
        }

        public static Percent operator +(Percent left, Percent right) => new Percent(left.value + right.value);
        public static Percent operator -(Percent left, Percent right) => new Percent(left.value - right.value);

        public static decimal operator *(Percent left, decimal right) => left.value * right / 100;
        public static decimal operator *(decimal left, Percent right) => left * right.value / 100;

        public static decimal operator /(Percent left, decimal right) => left.value / right * 100;
        public static decimal operator /(decimal left, Percent right) => left / right.value * 100;

        public static bool operator <=(Percent left, decimal right) => left.value / 100 <= right;
        public static bool operator <=(decimal left, Percent right) => left <= right.value / 100;

        public static bool operator >=(Percent left, decimal right) => left.value / 100 >= right;
        public static bool operator >=(decimal left, Percent right) => left >= right.value / 100;

        public static explicit operator decimal(Percent percent) => percent.value;

        public bool Equals(Percent other) => Math.Abs(value - other.value) < 0.01m;

        public override bool Equals(object obj) => obj is Percent other && Equals(other);

        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(Percent left, Percent right) => left.Equals(right);

        public static bool operator !=(Percent left, Percent right) => !left.Equals(right);

        public override string ToString() => $"{value}%";

        public int CompareTo(Percent other) => value.CompareTo(other.value);
        
        public static bool operator <(Percent left, Percent right) => left.CompareTo(right) < 0;

        public static bool operator >(Percent left, Percent right) => left.CompareTo(right) > 0;

        public static bool operator <=(Percent left, Percent right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Percent left, Percent right) => left.CompareTo(right) >= 0;

    }
}