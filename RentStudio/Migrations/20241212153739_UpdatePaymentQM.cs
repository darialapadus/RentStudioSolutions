using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentStudio.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentQM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdGuid",
                table: "PaymentsQueueMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserIdGuid",
                table: "PaymentsQueueMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
