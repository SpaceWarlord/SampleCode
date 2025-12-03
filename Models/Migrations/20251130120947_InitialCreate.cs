using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TPT");

            migrationBuilder.CreateTable(
                name: "Route",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreetType",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Common = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suburb",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PostCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UnitNum = table.Column<string>(type: "TEXT", nullable: true),
                    StreetNum = table.Column<string>(type: "TEXT", nullable: false),
                    StreetName = table.Column<string>(type: "TEXT", nullable: false),
                    StreetTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    SuburbId = table.Column<int>(type: "INTEGER", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    GPS = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_StreetType_StreetTypeId",
                        column: x => x.StreetTypeId,
                        principalSchema: "TPT",
                        principalTable: "StreetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Suburb_SuburbId",
                        column: x => x.SuburbId,
                        principalSchema: "TPT",
                        principalTable: "Suburb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteAddress",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "TPT",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteAddress_Route_RouteId",
                        column: x => x.RouteId,
                        principalSchema: "TPT",
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BaseFolder = table.Column<string>(type: "TEXT", nullable: false),
                    DarkMode = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "TPT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    SettingId = table.Column<int>(type: "INTEGER", nullable: true),
                    DefaultUser = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Setting_SettingId",
                        column: x => x.SettingId,
                        principalSchema: "TPT",
                        principalTable: "Setting",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_StreetTypeId",
                schema: "TPT",
                table: "Address",
                column: "StreetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_SuburbId",
                schema: "TPT",
                table: "Address",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAddress_AddressId",
                schema: "TPT",
                table: "RouteAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAddress_RouteId",
                schema: "TPT",
                table: "RouteAddress",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_UserId",
                schema: "TPT",
                table: "Setting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SettingId",
                schema: "TPT",
                table: "User",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "TPT",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Setting_User_UserId",
                schema: "TPT",
                table: "Setting",
                column: "UserId",
                principalSchema: "TPT",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Setting_User_UserId",
                schema: "TPT",
                table: "Setting");

            migrationBuilder.DropTable(
                name: "RouteAddress",
                schema: "TPT");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "TPT");

            migrationBuilder.DropTable(
                name: "Route",
                schema: "TPT");

            migrationBuilder.DropTable(
                name: "StreetType",
                schema: "TPT");

            migrationBuilder.DropTable(
                name: "Suburb",
                schema: "TPT");

            migrationBuilder.DropTable(
                name: "User",
                schema: "TPT");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "TPT");
        }
    }
}
