using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTask1.Migrations
{
    public partial class changeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Transport_companies_TcId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TKId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "TcId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Transport_companies_TcId",
                table: "Orders",
                column: "TcId",
                principalTable: "Transport_companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Transport_companies_TcId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "TcId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TKId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Transport_companies_TcId",
                table: "Orders",
                column: "TcId",
                principalTable: "Transport_companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
