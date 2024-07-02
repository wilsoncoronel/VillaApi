using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InsercionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle villa", new DateTime(2024, 6, 25, 19, 8, 24, 468, DateTimeKind.Local).AddTicks(6372), new DateTime(2024, 6, 25, 19, 8, 24, 468, DateTimeKind.Local).AddTicks(6362), "", 50, "Villa vista a la playa", 5, 200.0 },
                    { 2, "", "Detalle villa", new DateTime(2024, 6, 25, 19, 8, 24, 468, DateTimeKind.Local).AddTicks(6374), new DateTime(2024, 6, 25, 19, 8, 24, 468, DateTimeKind.Local).AddTicks(6373), "", 50, "Villa galante", 5, 200.0 },
                    { 3, "", "Detalle villa", new DateTime(2024, 6, 25, 19, 8, 24, 468, DateTimeKind.Local).AddTicks(6376), new DateTime(2024, 6, 25, 19, 8, 24, 468, DateTimeKind.Local).AddTicks(6375), "", 50, "Villa Nuvea", 4, 200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
