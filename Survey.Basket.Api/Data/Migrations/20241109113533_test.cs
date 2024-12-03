using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey.Basket.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateoFBirth",
                table: "Polls",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateoFBirth",
                table: "Polls");
        }
    }
}
