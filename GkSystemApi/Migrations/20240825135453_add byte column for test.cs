using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gk_system_api.Migrations
{
    /// <inheritdoc />
    public partial class addbytecolumnfortest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "GeneralConfig",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "GeneralConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "GeneralConfig");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "GeneralConfig");
        }
    }
}
