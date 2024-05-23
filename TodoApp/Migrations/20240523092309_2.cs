using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Clients_ClientId",
                table: "Rents");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Rents_ClientId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "DayPrice",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Rents",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Rents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Rents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DailyPrice",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRent",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    RentsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRent", x => new { x.EmployeesId, x.RentsId });
                    table.ForeignKey(
                        name: "FK_EmployeeRent_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRent_Rents_RentsId",
                        column: x => x.RentsId,
                        principalTable: "Rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CustomerId",
                table: "Rents",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_EmployeeId",
                table: "Companies",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRent_RentsId",
                table: "EmployeeRent",
                column: "RentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Employees_EmployeeId",
                table: "Companies",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Customers_CustomerId",
                table: "Rents",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Employees_EmployeeId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Customers_CustomerId",
                table: "Rents");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "EmployeeRent");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Rents_CustomerId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Companies_EmployeeId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DailyPrice",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Rents",
                newName: "ClientId");

            migrationBuilder.AddColumn<bool>(
                name: "DayPrice",
                table: "Rents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ClientId",
                table: "Rents",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Clients_ClientId",
                table: "Rents",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
