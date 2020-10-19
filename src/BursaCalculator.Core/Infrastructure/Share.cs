using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Share : IComparable<Share>
    {
        private readonly int value;

        public Share(int value)
        {
            this.value = value;
        }

        public static int operator /(Share left, int right) => left.value / right;
        
        public static explicit operator int(Share share) => share.value;

        public override string ToString() => $"{value} shares";

        public int CompareTo(Share other) => value.CompareTo(other.value);

        public static bool operator <(Share left, Share right) => left.CompareTo(right) < 0;

        public static bool operator >(Share left, Share right) => left.CompareTo(right) > 0;

        public static bool operator <=(Share left, Share right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Share left, Share right) => left.CompareTo(right) >= 0;
    }
}