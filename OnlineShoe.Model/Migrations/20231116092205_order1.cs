﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShoe.Model.Migrations
{
    /// <inheritdoc />
    public partial class order1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingFee",
                table: "Orders",
                type: "decimal(16,2)",
                precision: 16,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingFee",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)",
                oldPrecision: 16,
                oldScale: 2);
        }
    }
}
