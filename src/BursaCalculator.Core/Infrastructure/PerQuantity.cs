using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct PerQuantity : IEquatable<PerQuantity>, IComparable<PerQuantity>
    {
        private readonly decimal value;

        public PerQuantity(decimal value)
        {
            this.value = value;
        }

        public int CompareTo(PerQuantity other) => value.CompareTo(other.value);

        public override string ToString() => $"{value} MYR/share";

        public bool Equals(PerQuantity other) => value.Equals(other.value);

        public override bool Equals(object obj) => obj is PerQuantity other && Equals(other);

        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(PerQuantity left, PerQuantity right) => left.Equals(right);

        public static bool operator !=(PerQuantity left, PerQuantity right) => !left.Equals(right);

        public static decimal operator *(PerQuantity left, Quantity right) => left.value * right.Shares;


        public decimal PerShare => value;

        public decimal PerLot => value / 100;

    }
}