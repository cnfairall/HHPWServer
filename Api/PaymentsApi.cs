using HHPWServer.Models;
using Microsoft.EntityFrameworkCore;
namespace HHPWServer.Api
{
    public class PaymentsApi
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/api/payments/new", (HHPWServerDbContext db, Payment payment) =>
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                Order order = db.Orders
                                .Include(o => o.Items)
                                .SingleOrDefault(o => o.Id  == payment.OrderId);
                if (order == null)
                {
                    return Results.BadRequest("No order found");
                }
                if (order.IsClosed)
                {
                    return Results.BadRequest("Closed order cannot be modified");
                }
                if (order.Items.Count < 1)
                {
                    return Results.BadRequest("Cannot close empty order");
                }
                order.IsClosed = true;
                return Results.Created($"/api/payments/{payment.Id}", payment);
            });
        }
    }
}
