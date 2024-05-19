using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGRA2._0.Migrations
{
    /// <inheritdoc />
    public partial class Person : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Document",
                table: "persons",
                newName: "NumDocument");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumDocument",
                table: "persons",
                newName: "Document");
        }
    }
}
