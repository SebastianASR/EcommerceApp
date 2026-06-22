using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class AgregarGestionAvanzadaSolicitudesVip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archivada",
                table: "SolicitudesVip",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "SolicitudesVip",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaGestion",
                table: "SolicitudesVip",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotaInterna",
                table: "SolicitudesVip",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivada",
                table: "SolicitudesVip");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "SolicitudesVip");

            migrationBuilder.DropColumn(
                name: "FechaUltimaGestion",
                table: "SolicitudesVip");

            migrationBuilder.DropColumn(
                name: "NotaInterna",
                table: "SolicitudesVip");
        }
    }
}
