using Microsoft.EntityFrameworkCore;
using HHPWServer.Models;

    public class HHPWServerDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public HHPWServerDbContext(DbContextOptions<HHPWServerDbContext> context) : base(context)
            {

            }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order { Id = 1, CustEmail = "isotope47@msn.net", CustName = "Bill Blasky", IsClosed = true, OrderTypeId = 1, PhoneNum = "561-893-2219" },
                new Order { Id = 2, CustEmail = "popovili@yahoo.com", CustName = "Sarah Nitro", IsClosed = false, OrderTypeId = 2, PhoneNum = "561-338-9466" }
            });

            modelBuilder.Entity<Item>().HasData(new Item[]
            {
                new Item { Id = 1, ItemName = "Loaded Nachos", ItemPrice = 10.00M },
                new Item { Id = 2, ItemName = "Buffalo Wings", ItemPrice = 12.00M },
                new Item { Id = 3, ItemName = "Teriyaki Wings", ItemPrice = 12.00M },
                new Item { Id = 4, ItemName = "Pepperoni Pizza", ItemPrice = 15.00M },
                new Item { Id = 5, ItemName = "Bacon Ranch Pizza", ItemPrice = 17.00M }

            });

            modelBuilder.Entity<Payment>().HasData(new Payment[]
            {
                new Payment { Id = 1, OrderId = 1, OrderDate = new DateTime(2024, 4, 3), PaymentTypeId = 1, TipAmount = 10.00M },

            });

            modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
           {
               new PaymentType { Id = 1, Name = "cash" },
               new PaymentType { Id = 2, Name = "check" },
               new PaymentType { Id = 3, Name = "credit" },
               new PaymentType { Id = 4, Name = "debit" },
               new PaymentType { Id = 5, Name = "mobile pay" }
           });

            modelBuilder.Entity<OrderType>().HasData(new OrderType[]
           {
               new OrderType { Id = 1, Name = "in person" },
               new OrderType { Id = 2, Name = "phone" }
           });

            modelBuilder.Entity<User>().HasData(new User[]
          {
               new User { Id = 1, Uid = "HK475G8BK" },
               new User { Id = 2, Uid = "LL4910HEJ" }
          });
        }
    }
