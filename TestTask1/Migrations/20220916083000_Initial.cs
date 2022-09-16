using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTask1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transport_companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CoefficientOfKilometer = table.Column<double>(nullable: false),
                    CoefficientOfKilogram = table.Column<double>(nullable: false),
                    CoefficientOfSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    FirstPlace = table.Column<string>(nullable: true),
                    LastPlace = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Size = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Distance = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TcId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Transport_companies_TcId",
                        column: x => x.TcId,
                        principalTable: "Transport_companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Transport_companies",
                columns: new[] { "Id", "CoefficientOfKilogram", "CoefficientOfKilometer", "CoefficientOfSize", "Name" },
                values: new object[] { 1, 7.0, 1.5, 7.0, "Энергия" });

            migrationBuilder.InsertData(
                table: "Transport_companies",
                columns: new[] { "Id", "CoefficientOfKilogram", "CoefficientOfKilometer", "CoefficientOfSize", "Name" },
                values: new object[] { 2, 1.5, 7.0, 7.0, "СДЭК" });

            migrationBuilder.InsertData(
                table: "Transport_companies",
                columns: new[] { "Id", "CoefficientOfKilogram", "CoefficientOfKilometer", "CoefficientOfSize", "Name" },
                values: new object[] { 3, 7.0, 7.0, 1.5, "ПЭК" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TcId",
                table: "Orders",
                column: "TcId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Transport_companies");
        }
    }
}
