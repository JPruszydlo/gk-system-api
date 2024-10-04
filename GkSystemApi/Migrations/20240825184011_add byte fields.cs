using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gk_system_api.Migrations
{
    /// <inheritdoc />
    public partial class addbytefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByte",
                table: "References",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageByteType",
                table: "References",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByte",
                table: "RealisationImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageByteType",
                table: "RealisationImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByte",
                table: "OfferVisualisations",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageByteType",
                table: "OfferVisualisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "References");

            migrationBuilder.DropColumn(
                name: "ImageByteType",
                table: "References");

            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "RealisationImages");

            migrationBuilder.DropColumn(
                name: "ImageByteType",
                table: "RealisationImages");

            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "OfferVisualisations");

            migrationBuilder.DropColumn(
                name: "ImageByteType",
                table: "OfferVisualisations");
        }
    }
}
