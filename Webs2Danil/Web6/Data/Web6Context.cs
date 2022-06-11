using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web6.Models;

namespace Web6.Data
{
    public class Web6Context : DbContext
    {
        public Web6Context (DbContextOptions<Web6Context> options)
            : base(options)
        {
        }

        public DbSet<Web6.Models.ForumCategory>? ForumCategory { get; set; }
        public DbSet<Web6.Models.User>? User { get; set; }
        public DbSet<Web6.Models.Role>? Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRolename = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRolename };

            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Web6.Models.TopicPost>? TopicPost { get; set; }
        public DbSet<Web6.Models.Topic>? Topic { get; set; }

        
    }
}
