using HHPWServer.Models;
using Microsoft.EntityFrameworkCore;
namespace HHPWServer.Api
{
    public class PaymentsApi
    {
        public static void Map(WebApplication app)
        {
            //close order
            app.MapPost("/api/payments/new", (HHPWServerDbContext db, Payment payment) =>
            {
                Order order = db.Orders
                                .Include(o => o.Items)
                                .ThenInclude(i => i.Item)
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
                db.Payments.Add(payment);
                db.SaveChanges();
                return Results.Created($"/api/payments/{payment.Id}", payment);
            });

            //get total revenue
            app.MapGet("/api/payments/revenue", (HHPWServerDbContext db) =>
            {
                var orderTotals = db.Payments
                .Include(p => p.Order)
                .ThenInclude(o => o.Items)
                .ThenInclude(i => i.Item)
                .Select(p => p.OrderTotal).ToList();
                var revenue = orderTotals.Sum();
                                
                return revenue;
            });
        }
    }
}
