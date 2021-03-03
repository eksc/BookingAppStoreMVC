using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookingAppStoreMVC.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("UserConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        
    }

    class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            context.Roles.Add(new Role() { Id = 1, Name = "admin" });
            context.Roles.Add(new Role() { Id = 2, Name = "user" });
            context.Users.Add(new User
            {
                Id = 1,
                Email = "test@test.ru",
                Password = "qwerty",
                Age = 25,
                RoleId = 1
            });

            base.Seed(context);
        }
    }
}