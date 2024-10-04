using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gk_system_api.Migrations
{
    /// <inheritdoc />
    public partial class renamelocalisiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Localisations_LocalistionId",
                table: "Visitors");

            migrationBuilder.RenameColumn(
                name: "LocalistionId",
                table: "Visitors",
                newName: "LocalisationId");

            migrationBuilder.RenameIndex(
                name: "IX_Visitors_LocalistionId",
                table: "Visitors",
                newName: "IX_Visitors_LocalisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Localisations_LocalisationId",
                table: "Visitors",
                column: "LocalisationId",
                principalTable: "Localisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Localisations_LocalisationId",
                table: "Visitors");

            migrationBuilder.RenameColumn(
                name: "LocalisationId",
                table: "Visitors",
                newName: "LocalistionId");

            migrationBuilder.RenameIndex(
                name: "IX_Visitors_LocalisationId",
                table: "Visitors",
                newName: "IX_Visitors_LocalistionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Localisations_LocalistionId",
                table: "Visitors",
                column: "LocalistionId",
                principalTable: "Localisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
