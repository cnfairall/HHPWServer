using HHPWServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HHPWServer.Api
{
    public class OrdersApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/orders", (HHPWServerDbContext db) =>
            {
                return db.Orders;
            });

            app.MapGet("/api/orders/{orderId}", (HHPWServerDbContext db, int orderId) =>
            {
                Order order = db.Orders
                                .Include(o => o.Items)
                                .SingleOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    return Results.Ok(order);
                }
                return Results.BadRequest("No order found");
            });

            app.MapPost("/api/orders/new", (HHPWServerDbContext db, Order order) =>
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return Results.Created($"/api/orders/{order.Id}", order);
            });

            app.MapPut("/api/orders/{orderId}", (HHPWServerDbContext db, Order updatedOrder) =>
            {
                Order orderToEdit = db.Orders.SingleOrDefault(o => o.Id == updatedOrder.Id);
                if (orderToEdit != null)
                {
                    orderToEdit.CustEmail = updatedOrder.CustEmail;
                    orderToEdit.CustName = updatedOrder.CustName;
                    orderToEdit.PhoneNum = updatedOrder.PhoneNum;
                    orderToEdit.OrderTypeId = updatedOrder.OrderTypeId;

                    db.SaveChanges();
                    return Results.Ok(orderToEdit);
                }
                return Results.BadRequest("No order found");
            });

            app.MapDelete("/api/orders/{orderId}", (HHPWServerDbContext db, int orderId) =>
            {
                Order orderToDelete = db.Orders.SingleOrDefault(o => o.Id == orderId);
                db.Orders.Remove(orderToDelete);
                db.SaveChanges();
                return Results.Ok("Order deleted");
            });
        }
    }
}
