using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Lot : IEquatable<Lot>, IComparable<Lot>
    {
        internal readonly int Value;

        public Lot(int value)
        {
            this.Value = value;
        }

        public static int operator *(Lot left, int right) => left.Value * right * 100;
        public static int operator *(int left, Lot right) => left * right.Value * 100;
        public static decimal operator *(Lot left, decimal right) => left.Value * right * 100;
        public static decimal operator *(decimal left, Lot right) => left * right.Value * 100;

        public static Lot operator +(Lot left, Lot right) => new Lot(left.Value + right.Value);
        public static Lot operator -(Lot left, Lot right) => new Lot(left.Value - right.Value);

        public static explicit operator int(Lot lot) => lot.Value;

        public override string ToString() => $"{Value} lots";

        public int CompareTo(Lot other) => Value.CompareTo(other.Value);

        public static bool operator <(Lot left, Lot right) => left.CompareTo(right) < 0;

        public static bool operator >(Lot left, Lot right) => left.CompareTo(right) > 0;

        public static bool operator <=(Lot left, Lot right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Lot left, Lot right) => left.CompareTo(right) >= 0;

        public bool Equals(Lot other) => Value == other.Value;

        public override bool Equals(object obj) => obj is Lot other && Equals(other);

        public override int GetHashCode() => Value;

        public static bool operator ==(Lot left, Lot right) => left.Equals(right);

        public static bool operator !=(Lot left, Lot right) => !left.Equals(right);
    }
}