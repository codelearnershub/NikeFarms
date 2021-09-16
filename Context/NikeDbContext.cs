﻿using Microsoft.EntityFrameworkCore;
using NikeFarms.v2._0.Models;
using System;

namespace NikeFarms.Context
{
    public class NikeDbContext : DbContext
    {
        public NikeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DailyActivity> DailyActivities { get; set; }
        public DbSet<FlockType> FlockTypes { get; set; }
        public DbSet<Flock> Flocks { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<StoreAllocation> StoreAllocations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WeeklyReport> WeeklyReports { get; set; }

        public DbSet<Mortality> Mortalities { get; set; }

        public DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.HashSalt).IsRequired();

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();


            modelBuilder.Entity<User>().Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(40);
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.PhoneNo).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.Address).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.Gender).IsRequired();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Mazstar",
                    LastName = "Hamzy",
                    Address ="lag",
                    PhoneNo = "09055220828",
                    Email = "mazeedahhamzat@gmail.com",
                    Gender = "Female",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "mazeedahhamzat@gmail.com",
                    PasswordHash = "ZIoFJMM2K8J24l9L3yXaVYnFKKca+dIlRaPO9OJVB0g=",
                    HashSalt = "guDA4rCIrHxbmB2fm1lRCw==",
                }
                );


            modelBuilder.Entity<Role>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Super Admin",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "mazeedahhamzat@gmail.com",
                }
                );

            modelBuilder.Entity<Stock>().Property(u => u.ItemType).IsRequired();


            modelBuilder.Entity<Expenses>().Property(u => u.Description).IsRequired();


            modelBuilder.Entity<Message>().Property(u => u.Title).IsRequired();

            modelBuilder.Entity<Message>().Property(u => u.Content).IsRequired();

           
            modelBuilder.Entity<FlockType>().Property(u => u.Name).IsRequired();



            modelBuilder.Entity<Flock>().Property(u => u.BatchNo).IsRequired();

            modelBuilder.Entity<Flock>().HasIndex(u => u.BatchNo).IsUnique();




            modelBuilder.Entity<Sales>().Property(u => u.Item).IsRequired();


            



            modelBuilder.Entity<Notification>().Property(u => u.Content).IsRequired();

            //modelBuilder.Entity<Notification>().Property(u => u.CreatedBy).IsRequired();




            modelBuilder.Entity<SalesItem>().Property(u => u.Item).IsRequired();



            modelBuilder.Entity<StoreItem>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<StoreItem>().Property(u => u.ItemType).IsRequired();

            modelBuilder.Entity<UserRole>().HasIndex(u => new { u.UserId, u.RoleId }).IsUnique();

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    UserId = 1,
                    RoleId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "mazeedahhamzat@gmail.com",
                }
                );

            modelBuilder.Entity<Customer>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Customer>().Property(u => u.FirstName).IsRequired();

            modelBuilder.Entity<Customer>().Property(u => u.LastName).IsRequired();

            modelBuilder.Entity<Customer>().Property(u => u.PhoneNo).IsRequired();

            modelBuilder.Entity<Customer>().Property(u => u.Address).IsRequired();

            modelBuilder.Entity<Customer>().Property(u => u.Gender).IsRequired();

        }

       
    }
}
