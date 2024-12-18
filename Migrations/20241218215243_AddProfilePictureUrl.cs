using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSync.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePictureUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_SessionId",
                table: "Registrations",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Sessions_SessionId",
                table: "Registrations",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Sessions_SessionId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_SessionId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");
        }
    }
}
