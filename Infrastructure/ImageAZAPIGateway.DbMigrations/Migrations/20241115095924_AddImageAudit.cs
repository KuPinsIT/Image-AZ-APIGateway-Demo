using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageAZAPIGateway.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddImageAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "AZAPIGateway",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "AZAPIGateway",
                table: "Image",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                schema: "AZAPIGateway",
                table: "Image",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                schema: "AZAPIGateway",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "AZAPIGateway",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "AZAPIGateway",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "AZAPIGateway",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                schema: "AZAPIGateway",
                table: "Image");
        }
    }
}
