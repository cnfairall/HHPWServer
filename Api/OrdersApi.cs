﻿using HHPWServer.Models;
using HHPWServer.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HHPWServer.Api
{
    public class OrdersApi
    {
        public static void Map(WebApplication app)
        {
            //view all orders
            app.MapGet("/api/orders", (HHPWServerDbContext db) =>
            {
                return db.Orders;
            });

            //get single order with details
            app.MapGet("/api/orders/{orderId}", (HHPWServerDbContext db, int orderId) =>
            {
                Order order = db.Orders
                                .Include(o => o.Payment)
                                .Include(o => o.Items)
                                .ThenInclude(i => i.Item)
                                .SingleOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    return Results.Ok(order);
                }
                return Results.BadRequest("No order found");
            });

            //create new order
            app.MapPost("/api/orders/new", (HHPWServerDbContext db, Order order) =>
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return Results.Created($"/api/orders/{order.Id}", order);
            });

            //edit order (customer info)
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

            //delete order
            app.MapDelete("/api/orders/{orderId}", (HHPWServerDbContext db, int orderId) =>
            {
                Order orderToDelete = db.Orders.SingleOrDefault(o => o.Id == orderId);
                if (orderToDelete != null)
                {
                    db.Orders.Remove(orderToDelete);
                    db.SaveChanges();
                    return Results.Ok("Order deleted");
                }
                return Results.BadRequest("No order found");
            });

            //add item to order
            app.MapPatch("/api/orders/{orderId}/add", (HHPWServerDbContext db, OrderItemDto chosenItem) =>
            {
                Order order = db.Orders
                                .Include(o => o.Items)
                                .SingleOrDefault(o => o.Id == chosenItem.OrderId && o.IsClosed == false);
                Item itemToAdd = db.Items
                                    .SingleOrDefault(i => i.Id == chosenItem.ItemId);
                if (order == null)
                {
                    return Results.BadRequest("Invalid data submitted");
                }
                if (itemToAdd == null)
                {
                    return Results.BadRequest("No such item exists");
                }
                try
                {
                    OrderItem orderItem = new()
                    {
                        Item = itemToAdd,
                        Order = order,
                    };
                    order.Items.Add(orderItem);
                    db.SaveChanges();
                    return Results.Ok("item added");
                }
                catch (ArgumentNullException)
                {
                    return Results.BadRequest("Item not found");
                }
            });

            //remove item from order
            app.MapPatch("/api/orders/{orderId}/remove", (HHPWServerDbContext db, OrderItemDto chosenItem) =>
            {
                Order order = db.Orders
                            .Include(o => o.Items)
                            .SingleOrDefault(o => o.Id == chosenItem.OrderId && o.IsClosed == false);
                if (order == null)
                {
                    return Results.BadRequest("Invalid data submitted");
                }
                if (order.Items.Count < 1)
                {
                    return Results.BadRequest("Order is empty");
                }
                Item itemToRemove = db.Items
                                      .FirstOrDefault(i => i.Id == chosenItem.ItemId);
                if (itemToRemove == null)
                {
                    return Results.BadRequest("Item not found");
                }
                OrderItem orderItem = db.OrderItems.FirstOrDefault(i => i.Item.Id == itemToRemove.Id && i.Order.Id == order.Id);
                order.Items.Remove(orderItem);
                db.SaveChanges();
                return Results.Ok("Item removed");
            });

            //search orders
            app.MapGet("/api/orders/search/{searchInput}", (HHPWServerDbContext db, string searchInput) =>
            {
                List<Order> searchResults = db.Orders.Where(o => o.CustName.ToLower().Contains(searchInput) || o.PhoneNum.ToLower().Contains(searchInput)).ToList();
                if (searchResults != null)
                {
                    return Results.Ok(searchResults);
                }
                return Results.NotFound("no results found");
            });
        }
    }
}
