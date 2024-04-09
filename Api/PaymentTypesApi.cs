using HHPWServer.Models;

namespace HHPWServer.Api
{
    public class PaymentTypesApi
    {
        public static void Map(WebApplication app)
        {
            //get all payment types
            app.MapGet("/api/paymenttypes", (HHPWServerDbContext db) =>
            {
                return db.PaymentTypes;
            });

            //get single payment type
            app.MapGet("/api/paymenttypes/{ptId}", (HHPWServerDbContext db, int ptId) =>
            {
                PaymentType paymentType = db.PaymentTypes.SingleOrDefault(o => o.Id == ptId);
                if (paymentType == null)
                {
                    return null;
                }
                return paymentType;
            });
        }
    }
}
