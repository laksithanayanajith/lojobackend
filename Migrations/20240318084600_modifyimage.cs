using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lojobackend.Migrations
{
    /// <inheritdoc />
    public partial class modifyimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "images");
        }
    }
}
