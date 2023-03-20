using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class Temel_Tabloların_Güncellenmesi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto1",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto2",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto3",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto1",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "Foto2",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "Foto3",
                table: "Urunler");
        }
    }
}
