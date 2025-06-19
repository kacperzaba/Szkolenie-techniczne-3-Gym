using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Client.Storage.Migrations
{
    /// <inheritdoc />
    public partial class init131 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_CustomerId",
                table: "CustomerSubscriptions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_Customers_CustomerId",
                table: "CustomerSubscriptions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_Customers_CustomerId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_CustomerId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerSubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
