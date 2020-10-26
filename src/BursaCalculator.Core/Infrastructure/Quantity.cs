using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Quantity : IEquatable<Quantity>, IComparable<Quantity>
    {
        private readonly int value;

        internal Quantity(int value) => this.value = value;

        public override string ToString() => $"{value} units";

        public bool Equals(Quantity other) => value == other.value;

        public override bool Equals(object obj) => obj is Quantity other && Equals(other);

        public override int GetHashCode() => value;

        public static bool operator ==(Quantity left, Quantity right) => left.Equals(right);

        public static bool operator !=(Quantity left, Quantity right) => !left.Equals(right);

        public int CompareTo(Quantity other) => value.CompareTo(other.value);

        public static Quantity operator +(Quantity left, Quantity right) => new Quantity(left.value + right.value);
        public static Quantity operator -(Quantity left, Quantity right) => new Quantity(left.value - right.value);
        public static decimal operator /(Quantity left, Quantity right) => (decimal) left.value / right.value;

        public static Quantity operator *(Quantity left, int right) => new Quantity(left.value * right);
        public static Quantity operator *(int left, Quantity right) => new Quantity(left * right.value);

        public int Shares => value;

        public int Lots => value / 100;

        public Quantity Min(Quantity left, Quantity right) => new Quantity(Math.Min(left.value, right.value));
        public Quantity Max(Quantity left, Quantity right) => new Quantity(Math.Max(left.value, right.value));

        public static bool operator <(Quantity left, Quantity right) => left.value < right.value;

        public static bool operator >(Quantity left, Quantity right) => left.value > right.value;

        public static bool operator <=(Quantity left, Quantity right) => left.value <= right.value;

        public static bool operator >=(Quantity left, Quantity right) => left.value >= right.value;
    }
}