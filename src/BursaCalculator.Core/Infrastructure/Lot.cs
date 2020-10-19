using System;

namespace BursaCalculator.Core.Infrastructure
{
    public readonly struct Lot : IComparable<Lot>
    {
        private readonly int value;

        public Lot(int value)
        {
            this.value = value;
        }

        public static int operator *(Lot left, int right) => left.value * right * 100;
        public static int operator *(int left, Lot right) => left * right.value * 100;
        public static decimal operator *(Lot left, decimal right) => left.value * right * 100;
        public static decimal operator *(decimal left, Lot right) => left * right.value * 100;

        public static Lot operator +(Lot left, Lot right) => new Lot(left.value + right.value);
        public static Lot operator -(Lot left, Lot right) => new Lot(left.value - right.value);

        public static explicit operator int(Lot lot) => lot.value;

        public override string ToString() => $"{value} logs";
        /*
        public static decimal operator *(decimal left, ShareLot right) => left * right.Unit * 100;
        
        public static explicit operator ShareUnit(ShareLot lot) => lot * 100;
        */

        /*

        public static decimal operator /(Lot left, decimal right) => left.Unit / right * 100;
        public static decimal operator /(decimal left, Lot right) => left / right.Unit * 100;


        public static bool operator <=(Lot left, decimal right) => left.Unit * 100 <= right;
        public static bool operator <=(decimal left, Lot right) => left <= right.Unit * 100;
        public static bool operator <(Lot left, decimal right) => left.Unit * 100 < right;
        public static bool operator <(decimal left, Lot right) => left < right.Unit * 100;

        public static bool operator >=(Lot left, decimal right) => left.Unit * 100 >= right;
        public static bool operator >=(decimal left, Lot right) => left >= right.Unit * 100;
        public static bool operator >(Lot left, decimal right) => left.Unit * 100 > right;
        public static bool operator >(decimal left, Lot right) => left > right.Unit * 100;
    */
        public int CompareTo(Lot other) => value.CompareTo(other.value);

        public static bool operator <(Lot left, Lot right) => left.CompareTo(right) < 0;

        public static bool operator >(Lot left, Lot right) => left.CompareTo(right) > 0;

        public static bool operator <=(Lot left, Lot right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Lot left, Lot right) => left.CompareTo(right) >= 0;
    }
}