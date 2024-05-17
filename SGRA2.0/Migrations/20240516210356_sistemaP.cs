using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGRA2._0.Migrations
{
    /// <inheritdoc />
    public partial class sistemaP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personLogins",
                columns: table => new
                {
                    IdLoginP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personLogins", x => x.IdLoginP);
                    table.ForeignKey(
                        name: "FK_personLogins_persons_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "persons",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_personLogins_IdPerson",
                table: "personLogins",
                column: "IdPerson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personLogins");
        }
    }
}
