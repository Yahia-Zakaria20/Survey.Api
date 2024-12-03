using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey.Basket.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class removwprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateoFBirth",
                table: "Polls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateoFBirth",
                table: "Polls",
                type: "datetime2",
                nullable: true);
        }
    }
}
