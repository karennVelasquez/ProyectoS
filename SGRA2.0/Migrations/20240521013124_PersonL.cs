using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGRA2._0.Migrations
{
    /// <inheritdoc />
    public partial class PersonL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "personLogins",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "personLogins",
                newName: "UserName");
        }
    }
}
