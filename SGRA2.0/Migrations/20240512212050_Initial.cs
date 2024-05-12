using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGRA2._0.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "composters",
                columns: table => new
                {
                    IdComposter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrainageSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_composters", x => x.IdComposter);
                });

            migrationBuilder.CreateTable(
                name: "documentTypes",
                columns: table => new
                {
                    IdDocumentType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentTypes", x => x.IdDocumentType);
                });

            migrationBuilder.CreateTable(
                name: "levels",
                columns: table => new
                {
                    IdLevel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumLevel = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_levels", x => x.IdLevel);
                });

            migrationBuilder.CreateTable(
                name: "processStages",
                columns: table => new
                {
                    IdProcessStage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processStages", x => x.IdProcessStage);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "wasteTypes",
                columns: table => new
                {
                    IdWasteType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Waste_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descomposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wasteTypes", x => x.IdWasteType);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDocumentType = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.IdPerson);
                    table.ForeignKey(
                        name: "FK_persons_documentTypes_IdDocumentType",
                        column: x => x.IdDocumentType,
                        principalTable: "documentTypes",
                        principalColumn: "IdDocumentType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    IdGames = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLevel = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.IdGames);
                    table.ForeignKey(
                        name: "FK_games_levels_IdLevel",
                        column: x => x.IdLevel,
                        principalTable: "levels",
                        principalColumn: "IdLevel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wastes",
                columns: table => new
                {
                    IdWaste = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWasteType = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wastes", x => x.IdWaste);
                    table.ForeignKey(
                        name: "FK_wastes_wasteTypes_IdWasteType",
                        column: x => x.IdWasteType,
                        principalTable: "wasteTypes",
                        principalColumn: "IdWasteType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    IdCustomer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.IdCustomer);
                    table.ForeignKey(
                        name: "FK_customers_persons_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "persons",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_employees_persons_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "persons",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    IdSuppliers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    IdWasteType = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.IdSuppliers);
                    table.ForeignKey(
                        name: "FK_suppliers_persons_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "persons",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suppliers_wasteTypes_IdWasteType",
                        column: x => x.IdWasteType,
                        principalTable: "wasteTypes",
                        principalColumn: "IdWasteType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    IdAchievements = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdGames = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievements", x => x.IdAchievements);
                    table.ForeignKey(
                        name: "FK_achievements_games_IdGames",
                        column: x => x.IdGames,
                        principalTable: "games",
                        principalColumn: "IdGames",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_achievements_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "chemicalCompositions",
                columns: table => new
                {
                    IdChemicalComposition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    Chemical_Composition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chemicalCompositions", x => x.IdChemicalComposition);
                    table.ForeignKey(
                        name: "FK_chemicalCompositions_wastes_IdWaste",
                        column: x => x.IdWaste,
                        principalTable: "wastes",
                        principalColumn: "IdWaste",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "finalComposts",
                columns: table => new
                {
                    IdFinalCompost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    HumidityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalPh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nutrients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finalComposts", x => x.IdFinalCompost);
                    table.ForeignKey(
                        name: "FK_finalComposts_wastes_IdWaste",
                        column: x => x.IdWaste,
                        principalTable: "wastes",
                        principalColumn: "IdWaste",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "flips",
                columns: table => new
                {
                    IdFlip = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    Flipfrequency = table.Column<int>(type: "int", nullable: false),
                    UniformedDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flips", x => x.IdFlip);
                    table.ForeignKey(
                        name: "FK_flips_wastes_IdWaste",
                        column: x => x.IdWaste,
                        principalTable: "wastes",
                        principalColumn: "IdWaste",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recordTimes",
                columns: table => new
                {
                    IdRecordTime = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLevel = table.Column<int>(type: "int", nullable: false),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    Collecttime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountCollected = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recordTimes", x => x.IdRecordTime);
                    table.ForeignKey(
                        name: "FK_recordTimes_levels_IdLevel",
                        column: x => x.IdLevel,
                        principalTable: "levels",
                        principalColumn: "IdLevel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recordTimes_wastes_IdWaste",
                        column: x => x.IdWaste,
                        principalTable: "wastes",
                        principalColumn: "IdWaste",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "temperatures",
                columns: table => new
                {
                    IdTemperature = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    Decompositiontemperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temperatures", x => x.IdTemperature);
                    table.ForeignKey(
                        name: "FK_temperatures_wastes_IdWaste",
                        column: x => x.IdWaste,
                        principalTable: "wastes",
                        principalColumn: "IdWaste",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "times",
                columns: table => new
                {
                    IdTime = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    Processduration = table.Column<int>(type: "int", nullable: false),
                    IdProcessStage = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_times", x => x.IdTime);
                    table.ForeignKey(
                        name: "FK_times_processStages_IdProcessStage",
                        column: x => x.IdProcessStage,
                        principalTable: "processStages",
                        principalColumn: "IdProcessStage",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_times_wastes_IdWaste",
                        column: x => x.IdWaste,
                        principalTable: "wastes",
                        principalColumn: "IdWaste",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    IdSale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.IdSale);
                    table.ForeignKey(
                        name: "FK_sales_customers_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "customers",
                        principalColumn: "IdCustomer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "collectWastes",
                columns: table => new
                {
                    IdCollectWaste = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSuppliers = table.Column<int>(type: "int", nullable: false),
                    IdComposter = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectWastes", x => x.IdCollectWaste);
                    table.ForeignKey(
                        name: "FK_collectWastes_composters_IdComposter",
                        column: x => x.IdComposter,
                        principalTable: "composters",
                        principalColumn: "IdComposter",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_collectWastes_suppliers_IdSuppliers",
                        column: x => x.IdSuppliers,
                        principalTable: "suppliers",
                        principalColumn: "IdSuppliers",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    IdTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSuppliers = table.Column<int>(type: "int", nullable: false),
                    DeliveredQuantity = table.Column<int>(type: "int", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.IdTransaction);
                    table.ForeignKey(
                        name: "FK_transactions_suppliers_IdSuppliers",
                        column: x => x.IdSuppliers,
                        principalTable: "suppliers",
                        principalColumn: "IdSuppliers",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_IdGames",
                table: "achievements",
                column: "IdGames");

            migrationBuilder.CreateIndex(
                name: "IX_achievements_IdUser",
                table: "achievements",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_chemicalCompositions_IdWaste",
                table: "chemicalCompositions",
                column: "IdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_collectWastes_IdComposter",
                table: "collectWastes",
                column: "IdComposter");

            migrationBuilder.CreateIndex(
                name: "IX_collectWastes_IdSuppliers",
                table: "collectWastes",
                column: "IdSuppliers");

            migrationBuilder.CreateIndex(
                name: "IX_customers_IdPerson",
                table: "customers",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_employees_IdPerson",
                table: "employees",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_finalComposts_IdWaste",
                table: "finalComposts",
                column: "IdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_flips_IdWaste",
                table: "flips",
                column: "IdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_games_IdLevel",
                table: "games",
                column: "IdLevel");

            migrationBuilder.CreateIndex(
                name: "IX_persons_IdDocumentType",
                table: "persons",
                column: "IdDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_recordTimes_IdLevel",
                table: "recordTimes",
                column: "IdLevel");

            migrationBuilder.CreateIndex(
                name: "IX_recordTimes_IdWaste",
                table: "recordTimes",
                column: "IdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_sales_IdCustomer",
                table: "sales",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_IdPerson",
                table: "suppliers",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_IdWasteType",
                table: "suppliers",
                column: "IdWasteType");

            migrationBuilder.CreateIndex(
                name: "IX_temperatures_IdWaste",
                table: "temperatures",
                column: "IdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_times_IdProcessStage",
                table: "times",
                column: "IdProcessStage");

            migrationBuilder.CreateIndex(
                name: "IX_times_IdWaste",
                table: "times",
                column: "IdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_IdSuppliers",
                table: "transactions",
                column: "IdSuppliers");

            migrationBuilder.CreateIndex(
                name: "IX_wastes_IdWasteType",
                table: "wastes",
                column: "IdWasteType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "chemicalCompositions");

            migrationBuilder.DropTable(
                name: "collectWastes");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "finalComposts");

            migrationBuilder.DropTable(
                name: "flips");

            migrationBuilder.DropTable(
                name: "recordTimes");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "temperatures");

            migrationBuilder.DropTable(
                name: "times");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "composters");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "processStages");

            migrationBuilder.DropTable(
                name: "wastes");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "levels");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "wasteTypes");

            migrationBuilder.DropTable(
                name: "documentTypes");
        }
    }
}
