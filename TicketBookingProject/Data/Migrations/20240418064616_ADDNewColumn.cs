using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingProject.Migrations
{
    /// <inheritdoc />
    public partial class ADDNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailId",
                table: "UsersCredential",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailId",
                table: "UsersCredential");
        }
    }
}
