namespace BursaCalculator.Persistence
{
    public class Settings
    {
        public decimal Capital { get; set; }
        public decimal Risk { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal StopLossPrice { get; set; }
        public decimal TargetPrice { get; set; }
    }
}