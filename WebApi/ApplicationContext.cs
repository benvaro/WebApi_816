﻿using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated(); // створи БД при першому з'єднанні
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
