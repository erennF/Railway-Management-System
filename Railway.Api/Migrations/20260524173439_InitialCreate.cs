using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railway.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CountryID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "passenger",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PassengerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "route",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RouteName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.RouteID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.StaffID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "train",
                columns: table => new
                {
                    TrainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TrainID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "station",
                columns: table => new
                {
                    StationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StationType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.StationID);
                    table.ForeignKey(
                        name: "station_ibfk_1",
                        column: x => x.CountryID,
                        principalTable: "country",
                        principalColumn: "CountryID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "dependent",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    DependentID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.PassengerID, x.DependentID })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "dependent_ibfk_1",
                        column: x => x.PassengerID,
                        principalTable: "passenger",
                        principalColumn: "PassengerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "loyaltyaccount",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    TotalMiles = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    Class = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "'Bronze'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.AccountID);
                    table.ForeignKey(
                        name: "loyaltyaccount_ibfk_1",
                        column: x => x.PassengerID,
                        principalTable: "passenger",
                        principalColumn: "PassengerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "freighttrain",
                columns: table => new
                {
                    TrainID = table.Column<int>(type: "int", nullable: false),
                    MaxCargoWeight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TrainID);
                    table.ForeignKey(
                        name: "freighttrain_ibfk_1",
                        column: x => x.TrainID,
                        principalTable: "train",
                        principalColumn: "TrainID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "passengertrain",
                columns: table => new
                {
                    TrainID = table.Column<int>(type: "int", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TrainID);
                    table.ForeignKey(
                        name: "passengertrain_ibfk_1",
                        column: x => x.TrainID,
                        principalTable: "train",
                        principalColumn: "TrainID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "trainschedule",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainID = table.Column<int>(type: "int", nullable: false),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartureTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "trainschedule_ibfk_1",
                        column: x => x.TrainID,
                        principalTable: "train",
                        principalColumn: "TrainID");
                    table.ForeignKey(
                        name: "trainschedule_ibfk_2",
                        column: x => x.RouteID,
                        principalTable: "route",
                        principalColumn: "RouteID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "routestop",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.RouteID, x.StationID })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "routestop_ibfk_1",
                        column: x => x.RouteID,
                        principalTable: "route",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "routestop_ibfk_2",
                        column: x => x.StationID,
                        principalTable: "station",
                        principalColumn: "StationID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tracksegment",
                columns: table => new
                {
                    TrackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartStationID = table.Column<int>(type: "int", nullable: false),
                    EndStationID = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MaxPassengerSpeed = table.Column<int>(type: "int", nullable: false),
                    MaxFreightSpeed = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TrackID);
                    table.ForeignKey(
                        name: "tracksegment_ibfk_1",
                        column: x => x.StartStationID,
                        principalTable: "station",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "tracksegment_ibfk_2",
                        column: x => x.EndStationID,
                        principalTable: "station",
                        principalColumn: "StationID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "freightshipment",
                columns: table => new
                {
                    ShipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainID = table.Column<int>(type: "int", nullable: false),
                    Shipper = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ShipmentID);
                    table.ForeignKey(
                        name: "freightshipment_ibfk_1",
                        column: x => x.TrainID,
                        principalTable: "freighttrain",
                        principalColumn: "TrainID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ReservationStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ReservationID);
                    table.ForeignKey(
                        name: "reservation_ibfk_1",
                        column: x => x.PassengerID,
                        principalTable: "passenger",
                        principalColumn: "PassengerID");
                    table.ForeignKey(
                        name: "reservation_ibfk_2",
                        column: x => x.ScheduleID,
                        principalTable: "trainschedule",
                        principalColumn: "ScheduleID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "staffassignment",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.StaffID, x.ScheduleID })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "staffassignment_ibfk_1",
                        column: x => x.StaffID,
                        principalTable: "staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "staffassignment_ibfk_2",
                        column: x => x.ScheduleID,
                        principalTable: "trainschedule",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "waitinglist",
                columns: table => new
                {
                    WaitlistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.WaitlistID);
                    table.ForeignKey(
                        name: "waitinglist_ibfk_1",
                        column: x => x.PassengerID,
                        principalTable: "passenger",
                        principalColumn: "PassengerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "waitinglist_ibfk_2",
                        column: x => x.ScheduleID,
                        principalTable: "trainschedule",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "maintenancerecord",
                columns: table => new
                {
                    MaintenanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainID = table.Column<int>(type: "int", nullable: true),
                    TrackID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MaintenanceID);
                    table.ForeignKey(
                        name: "maintenancerecord_ibfk_1",
                        column: x => x.TrainID,
                        principalTable: "train",
                        principalColumn: "TrainID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "maintenancerecord_ibfk_2",
                        column: x => x.TrackID,
                        principalTable: "tracksegment",
                        principalColumn: "TrackID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "cargocontainer",
                columns: table => new
                {
                    ContainerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipmentID = table.Column<int>(type: "int", nullable: false),
                    ContainerType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ContainerID);
                    table.ForeignKey(
                        name: "cargocontainer_ibfk_1",
                        column: x => x.ShipmentID,
                        principalTable: "freightshipment",
                        principalColumn: "ShipmentID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "customsclearance",
                columns: table => new
                {
                    ClearanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipmentID = table.Column<int>(type: "int", nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ClearanceID);
                    table.ForeignKey(
                        name: "customsclearance_ibfk_1",
                        column: x => x.ShipmentID,
                        principalTable: "freightshipment",
                        principalColumn: "ShipmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "customsclearance_ibfk_2",
                        column: x => x.StationID,
                        principalTable: "station",
                        principalColumn: "StationID");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "luggage",
                columns: table => new
                {
                    LuggageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReservationID = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.LuggageID);
                    table.ForeignKey(
                        name: "luggage_ibfk_1",
                        column: x => x.ReservationID,
                        principalTable: "reservation",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReservationID = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoachType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.SeatID);
                    table.ForeignKey(
                        name: "seat_ibfk_1",
                        column: x => x.ReservationID,
                        principalTable: "reservation",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "ShipmentID",
                table: "cargocontainer",
                column: "ShipmentID");

            migrationBuilder.CreateIndex(
                name: "Name",
                table: "country",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ShipmentID1",
                table: "customsclearance",
                column: "ShipmentID");

            migrationBuilder.CreateIndex(
                name: "StationID",
                table: "customsclearance",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "TrainID",
                table: "freightshipment",
                column: "TrainID");

            migrationBuilder.CreateIndex(
                name: "PassengerID",
                table: "loyaltyaccount",
                column: "PassengerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ReservationID",
                table: "luggage",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "TrackID",
                table: "maintenancerecord",
                column: "TrackID");

            migrationBuilder.CreateIndex(
                name: "TrainID1",
                table: "maintenancerecord",
                column: "TrainID");

            migrationBuilder.CreateIndex(
                name: "Email",
                table: "passenger",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PassengerID1",
                table: "reservation",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "ScheduleID",
                table: "reservation",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "RouteName",
                table: "route",
                column: "RouteName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "StationID1",
                table: "routestop",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "ReservationID1",
                table: "seat",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "ScheduleID1",
                table: "staffassignment",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "CountryID",
                table: "station",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "EndStationID",
                table: "tracksegment",
                column: "EndStationID");

            migrationBuilder.CreateIndex(
                name: "StartStationID",
                table: "tracksegment",
                column: "StartStationID");

            migrationBuilder.CreateIndex(
                name: "RouteID",
                table: "trainschedule",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "TrainID2",
                table: "trainschedule",
                column: "TrainID");

            migrationBuilder.CreateIndex(
                name: "PassengerID2",
                table: "waitinglist",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "ScheduleID2",
                table: "waitinglist",
                column: "ScheduleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cargocontainer");

            migrationBuilder.DropTable(
                name: "customsclearance");

            migrationBuilder.DropTable(
                name: "dependent");

            migrationBuilder.DropTable(
                name: "loyaltyaccount");

            migrationBuilder.DropTable(
                name: "luggage");

            migrationBuilder.DropTable(
                name: "maintenancerecord");

            migrationBuilder.DropTable(
                name: "passengertrain");

            migrationBuilder.DropTable(
                name: "routestop");

            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropTable(
                name: "staffassignment");

            migrationBuilder.DropTable(
                name: "waitinglist");

            migrationBuilder.DropTable(
                name: "freightshipment");

            migrationBuilder.DropTable(
                name: "tracksegment");

            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "freighttrain");

            migrationBuilder.DropTable(
                name: "station");

            migrationBuilder.DropTable(
                name: "passenger");

            migrationBuilder.DropTable(
                name: "trainschedule");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "train");

            migrationBuilder.DropTable(
                name: "route");
        }
    }
}
