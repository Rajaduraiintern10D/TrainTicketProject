using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingProject.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationDbSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Image_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Image_Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainDetails",
                columns: table => new
                {
                    TrainNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DestinationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DestinationTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainDetails", x => x.TrainNumber);
                });

            migrationBuilder.CreateTable(
                name: "UsersCredential",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCredential", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "PassengerDetails",
                columns: table => new
                {
                    P_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passenger_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passenger_Age = table.Column<int>(type: "int", nullable: false),
                    Passenger_gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainNumber = table.Column<int>(type: "int", nullable: false),
                    StartingCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DestinationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DestinationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTicketCount = table.Column<int>(type: "int", nullable: false),
                    TotalFare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedTicketTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerDetails", x => x.P_Id);
                    table.ForeignKey(
                        name: "FK_PassengerDetails_TrainDetails_TrainNumber",
                        column: x => x.TrainNumber,
                        principalTable: "TrainDetails",
                        principalColumn: "TrainNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainWiseSeatAvailabilities",
                columns: table => new
                {
                    TrainNumber = table.Column<int>(type: "int", nullable: false),
                    FirstClassTotalSeat = table.Column<int>(type: "int", nullable: false),
                    FirstClassFare = table.Column<int>(type: "int", nullable: false),
                    SecondClassTotalSeat = table.Column<int>(type: "int", nullable: false),
                    SecondClassFare = table.Column<int>(type: "int", nullable: false),
                    SleeperClassTotalSeat = table.Column<int>(type: "int", nullable: false),
                    SleeperClassFare = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainWiseSeatAvailabilities", x => x.TrainNumber);
                    table.ForeignKey(
                        name: "FK_TrainWiseSeatAvailabilities_TrainDetails_TrainNumber",
                        column: x => x.TrainNumber,
                        principalTable: "TrainDetails",
                        principalColumn: "TrainNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerDetails_TrainNumber",
                table: "PassengerDetails",
                column: "TrainNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PassengerDetails");

            migrationBuilder.DropTable(
                name: "TrainWiseSeatAvailabilities");

            migrationBuilder.DropTable(
                name: "UsersCredential");

            migrationBuilder.DropTable(
                name: "TrainDetails");
        }
    }
}
