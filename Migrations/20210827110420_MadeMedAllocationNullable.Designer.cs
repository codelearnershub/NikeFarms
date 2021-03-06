// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NikeFarms.Context;

namespace NikeFarms.v2._0.Migrations
{
    [DbContext(typeof(NikeDbContext))]
    [Migration("20210827110420_MadeMedAllocationNullable")]
    partial class MadeMedAllocationNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NikeFarms.v2._0.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.DailyActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("FlockId")
                        .HasColumnType("int");

                    b.Property<int>("Mortality")
                        .HasColumnType("int");

                    b.Property<int>("NoOfFeedUsed")
                        .HasColumnType("int");

                    b.Property<int>("NoOfMedUsed")
                        .HasColumnType("int");

                    b.Property<int>("StoreAllocationFeedId")
                        .HasColumnType("int");

                    b.Property<int?>("StoreAllocationMedId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("FlockId");

                    b.HasIndex("StoreAllocationFeedId");

                    b.HasIndex("StoreAllocationMedId");

                    b.ToTable("DailyActivities");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Expenses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Flock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<decimal>("AmountPurchased")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("AvailableBirds")
                        .HasColumnType("int");

                    b.Property<double>("AverageWeight")
                        .HasColumnType("double");

                    b.Property<string>("BatchNo")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("FlockTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TotalNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("BatchNo")
                        .IsUnique();

                    b.HasIndex("FlockTypeId");

                    b.ToTable("Flocks");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.FlockType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("FlockTypes");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("RecieverId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("RecieverId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ApproveId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecieverId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("RecieverId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2021, 8, 27, 12, 4, 19, 132, DateTimeKind.Local).AddTicks(6861),
                            CreatedBy = "mazeedahhamzat@gmail.com",
                            Name = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Sales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSold")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Voucher")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.SalesItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NoOfItem")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerItem")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("SalesId")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("SalesId");

                    b.HasIndex("StockId");

                    b.ToTable("SalesItems");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AvailableItem")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("FlockId")
                        .HasColumnType("int");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("NoOfItem")
                        .HasColumnType("double");

                    b.Property<decimal?>("PricePerCrate")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("PricePerKg")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("FlockId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.StoreAllocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BatchNo")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<double?>("ItemPerKg")
                        .HasColumnType("double");

                    b.Property<double>("ItemRemaining")
                        .HasColumnType("double");

                    b.Property<string>("ItemType")
                        .HasColumnType("text");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<double>("NoOfItem")
                        .HasColumnType("double");

                    b.Property<int>("StoreItemId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("StoreItemId");

                    b.ToTable("StoreAllocations");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.StoreItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<double?>("ItemPerKg")
                        .HasColumnType("double");

                    b.Property<double>("ItemRemaining")
                        .HasColumnType("double");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("NoOfItem")
                        .HasColumnType("double");

                    b.Property<decimal>("TotalPricePurchased")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("StoreItems");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "lag",
                            CreatedAt = new DateTime(2021, 8, 27, 12, 4, 19, 128, DateTimeKind.Local).AddTicks(4765),
                            CreatedBy = "mazeedahhamzat@gmail.com",
                            Email = "mazeedahhamzat@gmail.com",
                            FirstName = "Mazstar",
                            Gender = "Female",
                            HashSalt = "guDA4rCIrHxbmB2fm1lRCw==",
                            LastName = "Hamzy",
                            PasswordHash = "ZIoFJMM2K8J24l9L3yXaVYnFKKca+dIlRaPO9OJVB0g=",
                            PhoneNo = "09055220828"
                        });
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2021, 8, 27, 12, 4, 19, 140, DateTimeKind.Local).AddTicks(1532),
                            CreatedBy = "mazeedahhamzat@gmail.com",
                            RoleId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.WeeklyReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AverageWeight")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("FlockId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("FlockId");

                    b.ToTable("WeeklyReports");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.DailyActivity", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.Flock", "Flock")
                        .WithMany()
                        .HasForeignKey("FlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NikeFarms.v2._0.Models.StoreAllocation", "StoreAllocationFeed")
                        .WithMany()
                        .HasForeignKey("StoreAllocationFeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NikeFarms.v2._0.Models.StoreAllocation", "StoreAllocationMed")
                        .WithMany()
                        .HasForeignKey("StoreAllocationMedId");
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Flock", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.FlockType", "FlockType")
                        .WithMany()
                        .HasForeignKey("FlockTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Message", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.User", "Reciever")
                        .WithMany()
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Notification", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.User", "Reciever")
                        .WithMany()
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Sales", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.SalesItem", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.Sales", "Sales")
                        .WithMany()
                        .HasForeignKey("SalesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NikeFarms.v2._0.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.Stock", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.Flock", "Flock")
                        .WithMany()
                        .HasForeignKey("FlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.StoreAllocation", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NikeFarms.v2._0.Models.StoreItem", "StoreItem")
                        .WithMany()
                        .HasForeignKey("StoreItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.UserRole", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NikeFarms.v2._0.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NikeFarms.v2._0.Models.WeeklyReport", b =>
                {
                    b.HasOne("NikeFarms.v2._0.Models.Flock", "Flock")
                        .WithMany()
                        .HasForeignKey("FlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
