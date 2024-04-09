using HHPWServer.Models;

namespace HHPWServer.Api
{
    public class OrderTypesApi
    {
        public static void Map(WebApplication app)
        {
            //get all order types
            app.MapGet("/api/ordertypes", (HHPWServerDbContext db) =>
            {
                return db.OrderTypes;
            });

            //get single order type
            app.MapGet("/api/ordertypes/{otId}", (HHPWServerDbContext db, int otId) =>
            {
                OrderType orderType = db.OrderTypes.SingleOrDefault(o => o.Id == otId);
                if (orderType == null)
                {
                    return null;
                }
                return orderType;
            });
        }
    }
}
