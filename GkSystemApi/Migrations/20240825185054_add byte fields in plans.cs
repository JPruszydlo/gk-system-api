using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gk_system_api.Migrations
{
    /// <inheritdoc />
    public partial class addbytefieldsinplans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByte",
                table: "OfferPlans",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageByteType",
                table: "OfferPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "OfferPlans");

            migrationBuilder.DropColumn(
                name: "ImageByteType",
                table: "OfferPlans");
        }
    }
}
