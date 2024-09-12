using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alicunde.PruebaTecnica.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddRetailersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "retailers",
                columns: table => new
                {
                    ReCode = table.Column<string>(type: "text", nullable: false),
                    ReName = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    CodingScheme = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_retailers", x => x.ReCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "retailers");
        }
    }
}
