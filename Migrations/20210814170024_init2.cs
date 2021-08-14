using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace NikeFarms.v2._0.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_FlockId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_StoreAllocationFeedId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_StoreAllocationMedId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_FlockTypeId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_ReceiverId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_CustomerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_SalesId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_StockId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_Stock_FlockId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_StoreItemId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_RoleId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_UserId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_WeeklyReport_FlockId",
                table: "BaseEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_StoreAllocationFeedId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_StoreAllocationMedId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_FlockTypeId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_ReceiverId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_CustomerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_SalesId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_StockId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_Stock_FlockId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_StoreItemId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_User_Email",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_RoleId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_UserId_RoleId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_WeeklyReport_FlockId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "FeedConsumedPerKg",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Mortality",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "NoOfMedUsed",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreAllocationFeedId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreAllocationMedId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "FlockTypeId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "TotalNo",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "FlockType_Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Role_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SalesItem_Item",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "NoOfItem",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PricePerItem",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AvailableItem",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Stock_FlockId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Stock_Item",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Stock_NoOfItem",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PricePerCrate",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PricePerKg",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ItemPerKg",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreAllocation_NoOfItem",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreItemId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreItem_ItemPerKg",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreItem_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StoreItem_NoOfItem",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "User_Address",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "User_Email",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "User_FirstName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "HashSalt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "User_LastName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "User_PhoneNo",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "WeeklyReport_AverageWeight",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "WeeklyReport_FlockId",
                table: "BaseEntity");

            migrationBuilder.RenameTable(
                name: "BaseEntity",
                newName: "WeeklyReports");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEntity_FlockId",
                table: "WeeklyReports",
                newName: "IX_WeeklyReports_FlockId");

            migrationBuilder.AlterColumn<double>(
                name: "AverageWeight",
                table: "WeeklyReports",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlockId",
                table: "WeeklyReports",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeeklyReports",
                table: "WeeklyReports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlockTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlockTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ItemType = table.Column<string>(nullable: false),
                    NoOfItem = table.Column<double>(nullable: false),
                    ItemPerKg = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 40, nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    HashSalt = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Item = table.Column<string>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    FlockTypeId = table.Column<int>(nullable: false),
                    BatchNo = table.Column<string>(nullable: false),
                    TotalNo = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    AverageWeight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flocks_FlockTypes_FlockTypeId",
                        column: x => x.FlockTypeId,
                        principalTable: "FlockTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    StoreItemId = table.Column<int>(nullable: false),
                    NoOfItem = table.Column<double>(nullable: false),
                    ItemPerKg = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreAllocations_StoreItems_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "StoreItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: true),
                    RecieverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Item = table.Column<string>(nullable: false),
                    NoOfItem = table.Column<double>(nullable: false),
                    AvailableItem = table.Column<double>(nullable: false),
                    PricePerCrate = table.Column<decimal>(nullable: true),
                    PricePerKg = table.Column<decimal>(nullable: true),
                    FlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Flocks_FlockId",
                        column: x => x.FlockId,
                        principalTable: "Flocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    FeedConsumedPerKg = table.Column<double>(nullable: false),
                    Mortality = table.Column<int>(nullable: false),
                    NoOfMedUsed = table.Column<int>(nullable: false),
                    FlockId = table.Column<int>(nullable: false),
                    StoreAllocationFeedId = table.Column<int>(nullable: false),
                    StoreAllocationMedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyActivities_Flocks_FlockId",
                        column: x => x.FlockId,
                        principalTable: "Flocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyActivities_StoreAllocations_StoreAllocationFeedId",
                        column: x => x.StoreAllocationFeedId,
                        principalTable: "StoreAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyActivities_StoreAllocations_StoreAllocationMedId",
                        column: x => x.StoreAllocationMedId,
                        principalTable: "StoreAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Item = table.Column<string>(nullable: false),
                    NoOfItem = table.Column<int>(nullable: false),
                    StockId = table.Column<int>(nullable: false),
                    SalesId = table.Column<int>(nullable: false),
                    PricePerItem = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesItems_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesItems_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyActivities_FlockId",
                table: "DailyActivities",
                column: "FlockId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyActivities_StoreAllocationFeedId",
                table: "DailyActivities",
                column: "StoreAllocationFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyActivities_StoreAllocationMedId",
                table: "DailyActivities",
                column: "StoreAllocationMedId");

            migrationBuilder.CreateIndex(
                name: "IX_Flocks_FlockTypeId",
                table: "Flocks",
                column: "FlockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverId",
                table: "Message",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItems_SalesId",
                table: "SalesItems",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItems_StockId",
                table: "SalesItems",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_FlockId",
                table: "Stocks",
                column: "FlockId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreAllocations_StoreItemId",
                table: "StoreAllocations",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyReports_Flocks_FlockId",
                table: "WeeklyReports",
                column: "FlockId",
                principalTable: "Flocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyReports_Flocks_FlockId",
                table: "WeeklyReports");

            migrationBuilder.DropTable(
                name: "DailyActivities");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "SalesItems");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "StoreAllocations");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StoreItems");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Flocks");

            migrationBuilder.DropTable(
                name: "FlockTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeeklyReports",
                table: "WeeklyReports");

            migrationBuilder.RenameTable(
                name: "WeeklyReports",
                newName: "BaseEntity");

            migrationBuilder.RenameIndex(
                name: "IX_WeeklyReports_FlockId",
                table: "BaseEntity",
                newName: "IX_BaseEntity_FlockId");

            migrationBuilder.AlterColumn<int>(
                name: "FlockId",
                table: "BaseEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "AverageWeight",
                table: "BaseEntity",
                type: "double",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FeedConsumedPerKg",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mortality",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfMedUsed",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreAllocationFeedId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreAllocationMedId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BaseEntity",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlockTypeId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalNo",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlockType_Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecieverId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role_Name",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "BaseEntity",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesItem_Item",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfItem",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerItem",
                table: "BaseEntity",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AvailableItem",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock_FlockId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stock_Item",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Stock_NoOfItem",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerCrate",
                table: "BaseEntity",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerKg",
                table: "BaseEntity",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ItemPerKg",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StoreAllocation_NoOfItem",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreItemId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StoreItem_ItemPerKg",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreItem_Name",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StoreItem_NoOfItem",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Address",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Email",
                table: "BaseEntity",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_FirstName",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashSalt",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_LastName",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_PhoneNo",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WeeklyReport_AverageWeight",
                table: "BaseEntity",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyReport_FlockId",
                table: "BaseEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_StoreAllocationFeedId",
                table: "BaseEntity",
                column: "StoreAllocationFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_StoreAllocationMedId",
                table: "BaseEntity",
                column: "StoreAllocationMedId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_FlockTypeId",
                table: "BaseEntity",
                column: "FlockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_ReceiverId",
                table: "BaseEntity",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_CustomerId",
                table: "BaseEntity",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_SalesId",
                table: "BaseEntity",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_StockId",
                table: "BaseEntity",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Stock_FlockId",
                table: "BaseEntity",
                column: "Stock_FlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_StoreItemId",
                table: "BaseEntity",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_User_Email",
                table: "BaseEntity",
                column: "User_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_RoleId",
                table: "BaseEntity",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_UserId_RoleId",
                table: "BaseEntity",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_WeeklyReport_FlockId",
                table: "BaseEntity",
                column: "WeeklyReport_FlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_FlockId",
                table: "BaseEntity",
                column: "FlockId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_StoreAllocationFeedId",
                table: "BaseEntity",
                column: "StoreAllocationFeedId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_StoreAllocationMedId",
                table: "BaseEntity",
                column: "StoreAllocationMedId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_FlockTypeId",
                table: "BaseEntity",
                column: "FlockTypeId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_ReceiverId",
                table: "BaseEntity",
                column: "ReceiverId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_CustomerId",
                table: "BaseEntity",
                column: "CustomerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_SalesId",
                table: "BaseEntity",
                column: "SalesId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_StockId",
                table: "BaseEntity",
                column: "StockId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_Stock_FlockId",
                table: "BaseEntity",
                column: "Stock_FlockId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_StoreItemId",
                table: "BaseEntity",
                column: "StoreItemId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_RoleId",
                table: "BaseEntity",
                column: "RoleId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_UserId",
                table: "BaseEntity",
                column: "UserId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_WeeklyReport_FlockId",
                table: "BaseEntity",
                column: "WeeklyReport_FlockId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
