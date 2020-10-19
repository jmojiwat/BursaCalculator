using LiteDB;

namespace BursaCalculator.Persistence
{
    public static class Database
    {
        private static readonly string DatabaseLocation = @"D:\Projects\BursaCalculator\Fees.db";

        public static ILiteCollection<DbFee> GetFees()
        {
            using var db = new LiteDatabase(DatabaseLocation);
            var fees = db.GetCollection<DbFee>("fees");

            return fees.Count() == 0 ? PersistDefaultFees(fees) : fees;
        }

        private static ILiteCollection<DbFee> PersistDefaultFees(ILiteCollection<DbFee> fees)
        {
            fees.InsertBulk(Data.DefaultFeeProfiles());
            return fees;
        }
    }
}