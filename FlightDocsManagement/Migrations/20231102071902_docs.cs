using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsService.Migrations
{
    /// <inheritdoc />
    public partial class docs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Docs",
                columns: table => new
                {
                    DocsName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docs", x => x.DocsName);
                });

            migrationBuilder.CreateTable(
                name: "DocsPermissions",
                columns: table => new
                {
                    DocsName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsPermissions", x => new { x.DocsName, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "DocsTypes",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlaneId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PointOfLoading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfUploading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Docs");

            migrationBuilder.DropTable(
                name: "DocsPermissions");

            migrationBuilder.DropTable(
                name: "DocsTypes");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
