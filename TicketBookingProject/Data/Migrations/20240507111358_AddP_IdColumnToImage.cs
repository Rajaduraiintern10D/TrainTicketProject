using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingProject.Migrations
{
    /// <inheritdoc />
    public partial class AddP_IdColumnToImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "P_Id",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "P_Id",
                table: "Images");
        }

    }
}
