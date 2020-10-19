using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Tick : IEquatable<Tick>, IComparable<Tick>
    {
        private readonly int value;

        public Tick(int value)
        {
            this.value = value;
        }

        public static Tick operator +(Tick left, Tick right) => new Tick(left.value + right.value);
        public static Tick operator -(Tick left, Tick right) => new Tick(left.value - right.value);


        public override string ToString() => $"{value} ticks";

        public static explicit operator int(Tick tick) => tick.value;
        /*
        public static decimal operator *(Tick left, decimal right) => left.Unit * right * 100;
        public static decimal operator *(decimal left, Tick right) => left * right.Unit * 100;

        public static decimal operator /(Tick left, decimal right) => left.Unit / right * 100;
        public static decimal operator /(decimal left, Tick right) => left / right.Unit * 100;

        public static Tick operator +(Tick left, Tick right) => new Tick(left.Unit + right.Unit);
        public static Tick operator -(Tick left, Tick right) => new Tick(left.Unit - right.Unit);

        public static bool operator <=(Tick left, decimal right) => left.Unit * 100 <= right;
        public static bool operator <=(decimal left, Tick right) => left <= right.Unit * 100;
        public static bool operator <(Tick left, decimal right) => left.Unit * 100 < right;
        public static bool operator <(decimal left, Tick right) => left < right.Unit * 100;

        public static bool operator >=(Tick left, decimal right) => left.Unit * 100 >= right;
        public static bool operator >=(decimal left, Tick right) => left >= right.Unit * 100;
        public static bool operator >(Tick left, decimal right) => left.Unit * 100 > right;
        public static bool operator >(decimal left, Tick right) => left > right.Unit * 100;
    */
        public bool Equals(Tick other) => value == other.value;

        public override bool Equals(object obj) => obj is Tick other && Equals(other);

        public override int GetHashCode() => value;

        public static bool operator ==(Tick left, Tick right) => left.Equals(right);

        public static bool operator !=(Tick left, Tick right) => !left.Equals(right);

        public int CompareTo(Tick other) => value.CompareTo(other.value);

        public static bool operator <(Tick left, Tick right) => left.CompareTo(right) < 0;

        public static bool operator >(Tick left, Tick right) => left.CompareTo(right) > 0;

        public static bool operator <=(Tick left, Tick right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Tick left, Tick right) => left.CompareTo(right) >= 0;
    }
}