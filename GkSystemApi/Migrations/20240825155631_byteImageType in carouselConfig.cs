using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gk_system_api.Migrations
{
    /// <inheritdoc />
    public partial class byteImageTypeincarouselConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ByteImageType",
                table: "CarouselConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByteImageType",
                table: "CarouselConfig");
        }
    }
}
