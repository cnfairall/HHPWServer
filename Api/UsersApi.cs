using HHPWServer.Dtos;
using HHPWServer.Models;

namespace HHPWServer.Api
{
    public class UsersApi
    {
        public static void Map(WebApplication app)
        {
            //check user
            app.MapPost("/api/checkuser", (HHPWServerDbContext db, UserAuthDto uid) =>
            {
                User user = db.Users.SingleOrDefault(u => u.Uid == uid.Uid);

                if (user != null)
                {
                    return Results.Ok(user);              
                }
                else
                {
                    User newUser = new User();
                    newUser.Uid = uid.Uid;
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return Results.Created($"/users/{newUser.Id}", newUser);
                }

            });

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
        }
    }
}
