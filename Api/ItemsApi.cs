using HHPWServer.Models;
namespace HHPWServer.Api
{
    public class ItemsApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/items", (HHPWServerDbContext db) =>
            {
                return db.Items;
            });

            app.MapGet("/api/items/{itemId}", (HHPWServerDbContext db, int itemId) =>
            {
                Item singleItem = db.Items.SingleOrDefault(i => i.Id == itemId);
                if (singleItem != null)
                {
                    return singleItem;
                }
                return null;
            });
        }
    }
}
