using HHPWServer.Models;

namespace HHPWServer.Api
{
    public class UsersApi
    {
        public static void Map(WebApplication app)
        {
            //get single user
            app.MapGet("/api/users/{userId}", (HHPWServerDbContext db, int userId) =>
            {
                User user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    return Results.Ok(user);
                }
                return Results.NotFound("No user found");
            });

            //create user
            app.MapPost("/api/users/new", (HHPWServerDbContext db, User user) =>
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Results.Ok(user);
            });
        }
    }
}
