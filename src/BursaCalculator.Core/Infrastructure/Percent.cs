using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Percent : IEquatable<Percent>, IComparable<Percent>
    {
        internal readonly decimal Value;

        public Percent(decimal value)
        {
            this.Value = value;
        }

        public static Percent operator +(Percent left, Percent right) => new Percent(left.Value + right.Value);
        public static Percent operator -(Percent left, Percent right) => new Percent(left.Value - right.Value);

        public static decimal operator *(Percent left, decimal right) => left.Value * right / 100;
        public static decimal operator *(decimal left, Percent right) => left * right.Value / 100;

        public static decimal operator /(Percent left, decimal right) => left.Value / right * 100;
        public static decimal operator /(decimal left, Percent right) => left / right.Value * 100;

        public static bool operator <=(Percent left, decimal right) => left.Value / 100 <= right;
        public static bool operator <=(decimal left, Percent right) => left <= right.Value / 100;

        public static bool operator >=(Percent left, decimal right) => left.Value / 100 >= right;
        public static bool operator >=(decimal left, Percent right) => left >= right.Value / 100;

        public static explicit operator decimal(Percent percent) => percent.Value;

        public bool Equals(Percent other) => Math.Abs(Value - other.Value) < 0.01m;

        public bool Equals(Percent other, decimal epsilon) => Math.Abs(Value - other.Value) < epsilon;

        public override bool Equals(object obj) => obj is Percent other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Percent left, Percent right) => left.Equals(right);

        public static bool operator !=(Percent left, Percent right) => !left.Equals(right);

        public override string ToString() => $"{Value}%";

        public int CompareTo(Percent other) => Value.CompareTo(other.Value);
        
        public static bool operator <(Percent left, Percent right) => left.Value < right.Value;

        public static bool operator >(Percent left, Percent right) => left.Value > right.Value;

        public static bool operator <=(Percent left, Percent right) => left.Value <= right.Value;

        public static bool operator >=(Percent left, Percent right) => left.Value >= right.Value;

    }
}