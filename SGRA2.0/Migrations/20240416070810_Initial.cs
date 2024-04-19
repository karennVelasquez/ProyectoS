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
                name: "achievements",
                columns: table => new
                {
                    IdAchievements = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Achievement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievements", x => x.IdAchievements);
                });

            migrationBuilder.CreateTable(
                name: "composters",
                columns: table => new
                {
                    IdComposter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrainageSystem = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    NumLevel = table.Column<int>(type: "int", nullable: false)
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
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Descomposition = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    DocumentTypeIdDocumentType = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.IdPerson);
                    table.ForeignKey(
                        name: "FK_persons_documentTypes_DocumentTypeIdDocumentType",
                        column: x => x.DocumentTypeIdDocumentType,
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
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    UserIdUser = table.Column<int>(type: "int", nullable: false),
                    IdLevel = table.Column<int>(type: "int", nullable: false),
                    LevelIdLevel = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.IdGames);
                    table.ForeignKey(
                        name: "FK_games_levels_LevelIdLevel",
                        column: x => x.LevelIdLevel,
                        principalTable: "levels",
                        principalColumn: "IdLevel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_users_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wastes",
                columns: table => new
                {
                    IdWaste = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWasteType = table.Column<int>(type: "int", nullable: false),
                    WasteTypeIdWasteType = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wastes", x => x.IdWaste);
                    table.ForeignKey(
                        name: "FK_wastes_wasteTypes_WasteTypeIdWasteType",
                        column: x => x.WasteTypeIdWasteType,
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
                    PersonIdPerson = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.IdCustomer);
                    table.ForeignKey(
                        name: "FK_customers_persons_PersonIdPerson",
                        column: x => x.PersonIdPerson,
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
                    PersonIdPerson = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_employees_persons_PersonIdPerson",
                        column: x => x.PersonIdPerson,
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
                    PersonIdPerson = table.Column<int>(type: "int", nullable: false),
                    IdWasteType = table.Column<int>(type: "int", nullable: false),
                    WasteTypeIdWasteType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.IdSuppliers);
                    table.ForeignKey(
                        name: "FK_suppliers_persons_PersonIdPerson",
                        column: x => x.PersonIdPerson,
                        principalTable: "persons",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suppliers_wasteTypes_WasteTypeIdWasteType",
                        column: x => x.WasteTypeIdWasteType,
                        principalTable: "wasteTypes",
                        principalColumn: "IdWasteType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "achievementsGames",
                columns: table => new
                {
                    IdAchievementsG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGames = table.Column<int>(type: "int", nullable: false),
                    GamesIdGames = table.Column<int>(type: "int", nullable: false),
                    IdAchievements = table.Column<int>(type: "int", nullable: false),
                    AchievementsIdAchievements = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievementsGames", x => x.IdAchievementsG);
                    table.ForeignKey(
                        name: "FK_achievementsGames_achievements_AchievementsIdAchievements",
                        column: x => x.AchievementsIdAchievements,
                        principalTable: "achievements",
                        principalColumn: "IdAchievements",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_achievementsGames_games_GamesIdGames",
                        column: x => x.GamesIdGames,
                        principalTable: "games",
                        principalColumn: "IdGames",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scores",
                columns: table => new
                {
                    IdPuntaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    UserIdUser = table.Column<int>(type: "int", nullable: false),
                    IdGames = table.Column<int>(type: "int", nullable: false),
                    GamesIdGames = table.Column<int>(type: "int", nullable: false),
                    NumScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scores", x => x.IdPuntaje);
                    table.ForeignKey(
                        name: "FK_scores_games_GamesIdGames",
                        column: x => x.GamesIdGames,
                        principalTable: "games",
                        principalColumn: "IdGames",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scores_users_UserIdUser",
                        column: x => x.UserIdUser,
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
                    WasteIdWaste = table.Column<int>(type: "int", nullable: false),
                    Chemical_Composition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chemicalCompositions", x => x.IdChemicalComposition);
                    table.ForeignKey(
                        name: "FK_chemicalCompositions_wastes_WasteIdWaste",
                        column: x => x.WasteIdWaste,
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
                    WasteIdWaste = table.Column<int>(type: "int", nullable: false),
                    HumidityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalPh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nutrients = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finalComposts", x => x.IdFinalCompost);
                    table.ForeignKey(
                        name: "FK_finalComposts_wastes_WasteIdWaste",
                        column: x => x.WasteIdWaste,
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
                    WasteIdWaste = table.Column<int>(type: "int", nullable: false),
                    Flipfrequency = table.Column<int>(type: "int", nullable: false),
                    UniformedDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flips", x => x.IdFlip);
                    table.ForeignKey(
                        name: "FK_flips_wastes_WasteIdWaste",
                        column: x => x.WasteIdWaste,
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
                    LevelIdLevel = table.Column<int>(type: "int", nullable: false),
                    IdWaste = table.Column<int>(type: "int", nullable: false),
                    WasteIdWaste = table.Column<int>(type: "int", nullable: false),
                    Collecttime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountCollected = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recordTimes", x => x.IdRecordTime);
                    table.ForeignKey(
                        name: "FK_recordTimes_levels_LevelIdLevel",
                        column: x => x.LevelIdLevel,
                        principalTable: "levels",
                        principalColumn: "IdLevel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recordTimes_wastes_WasteIdWaste",
                        column: x => x.WasteIdWaste,
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
                    WasteIdWaste = table.Column<int>(type: "int", nullable: false),
                    Decompositiontemperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temperatures", x => x.IdTemperature);
                    table.ForeignKey(
                        name: "FK_temperatures_wastes_WasteIdWaste",
                        column: x => x.WasteIdWaste,
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
                    WasteIdWaste = table.Column<int>(type: "int", nullable: false),
                    Processduration = table.Column<int>(type: "int", nullable: false),
                    IdProcessStage = table.Column<int>(type: "int", nullable: false),
                    EtapProcessStageaProcesoIdProcessStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_times", x => x.IdTime);
                    table.ForeignKey(
                        name: "FK_times_processStages_EtapProcessStageaProcesoIdProcessStage",
                        column: x => x.EtapProcessStageaProcesoIdProcessStage,
                        principalTable: "processStages",
                        principalColumn: "IdProcessStage",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_times_wastes_WasteIdWaste",
                        column: x => x.WasteIdWaste,
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
                    CustomerIdCustomer = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.IdSale);
                    table.ForeignKey(
                        name: "FK_sales_customers_CustomerIdCustomer",
                        column: x => x.CustomerIdCustomer,
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
                    SuppliersIdSuppliers = table.Column<int>(type: "int", nullable: false),
                    IdComposter = table.Column<int>(type: "int", nullable: false),
                    ComposterIdComposter = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectWastes", x => x.IdCollectWaste);
                    table.ForeignKey(
                        name: "FK_collectWastes_composters_ComposterIdComposter",
                        column: x => x.ComposterIdComposter,
                        principalTable: "composters",
                        principalColumn: "IdComposter",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_collectWastes_suppliers_SuppliersIdSuppliers",
                        column: x => x.SuppliersIdSuppliers,
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
                    SuppliersIdSuppliers = table.Column<int>(type: "int", nullable: false),
                    DeliveredQuantity = table.Column<int>(type: "int", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.IdTransaction);
                    table.ForeignKey(
                        name: "FK_transactions_suppliers_SuppliersIdSuppliers",
                        column: x => x.SuppliersIdSuppliers,
                        principalTable: "suppliers",
                        principalColumn: "IdSuppliers",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievementsGames_AchievementsIdAchievements",
                table: "achievementsGames",
                column: "AchievementsIdAchievements");

            migrationBuilder.CreateIndex(
                name: "IX_achievementsGames_GamesIdGames",
                table: "achievementsGames",
                column: "GamesIdGames");

            migrationBuilder.CreateIndex(
                name: "IX_chemicalCompositions_WasteIdWaste",
                table: "chemicalCompositions",
                column: "WasteIdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_collectWastes_ComposterIdComposter",
                table: "collectWastes",
                column: "ComposterIdComposter");

            migrationBuilder.CreateIndex(
                name: "IX_collectWastes_SuppliersIdSuppliers",
                table: "collectWastes",
                column: "SuppliersIdSuppliers");

            migrationBuilder.CreateIndex(
                name: "IX_customers_PersonIdPerson",
                table: "customers",
                column: "PersonIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_employees_PersonIdPerson",
                table: "employees",
                column: "PersonIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_finalComposts_WasteIdWaste",
                table: "finalComposts",
                column: "WasteIdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_flips_WasteIdWaste",
                table: "flips",
                column: "WasteIdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_games_LevelIdLevel",
                table: "games",
                column: "LevelIdLevel");

            migrationBuilder.CreateIndex(
                name: "IX_games_UserIdUser",
                table: "games",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_persons_DocumentTypeIdDocumentType",
                table: "persons",
                column: "DocumentTypeIdDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_recordTimes_LevelIdLevel",
                table: "recordTimes",
                column: "LevelIdLevel");

            migrationBuilder.CreateIndex(
                name: "IX_recordTimes_WasteIdWaste",
                table: "recordTimes",
                column: "WasteIdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_sales_CustomerIdCustomer",
                table: "sales",
                column: "CustomerIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_scores_GamesIdGames",
                table: "scores",
                column: "GamesIdGames");

            migrationBuilder.CreateIndex(
                name: "IX_scores_UserIdUser",
                table: "scores",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_PersonIdPerson",
                table: "suppliers",
                column: "PersonIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_WasteTypeIdWasteType",
                table: "suppliers",
                column: "WasteTypeIdWasteType");

            migrationBuilder.CreateIndex(
                name: "IX_temperatures_WasteIdWaste",
                table: "temperatures",
                column: "WasteIdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_times_EtapProcessStageaProcesoIdProcessStage",
                table: "times",
                column: "EtapProcessStageaProcesoIdProcessStage");

            migrationBuilder.CreateIndex(
                name: "IX_times_WasteIdWaste",
                table: "times",
                column: "WasteIdWaste");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_SuppliersIdSuppliers",
                table: "transactions",
                column: "SuppliersIdSuppliers");

            migrationBuilder.CreateIndex(
                name: "IX_wastes_WasteTypeIdWasteType",
                table: "wastes",
                column: "WasteTypeIdWasteType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "achievementsGames");

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
                name: "scores");

            migrationBuilder.DropTable(
                name: "temperatures");

            migrationBuilder.DropTable(
                name: "times");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "composters");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "processStages");

            migrationBuilder.DropTable(
                name: "wastes");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "levels");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "wasteTypes");

            migrationBuilder.DropTable(
                name: "documentTypes");
        }
    }
}
