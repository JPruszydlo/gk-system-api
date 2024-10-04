using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gk_system_api.Migrations
{
    /// <inheritdoc />
    public partial class add_ticks_to_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SendAt",
                table: "Emails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendAt",
                table: "Emails");
        }
    }
}
