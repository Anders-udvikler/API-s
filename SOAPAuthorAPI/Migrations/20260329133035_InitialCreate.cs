using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.SoapApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tauthor",
                columns: table => new
                {
                    nAuthorID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cName = table.Column<string>(type: "TEXT", nullable: false),
                    cSurname = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tauthor", x => x.nAuthorID);
                });

            migrationBuilder.CreateTable(
                name: "tbook",
                columns: table => new
                {
                    nBookID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cTitle = table.Column<string>(type: "TEXT", nullable: false),
                    nAuthorID = table.Column<int>(type: "INTEGER", nullable: false),
                    nPublishingCompanyID = table.Column<int>(type: "INTEGER", nullable: false),
                    nPublishingYear = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbook", x => x.nBookID);
                });

            migrationBuilder.CreateTable(
                name: "tpublishingcompany",
                columns: table => new
                {
                    nPublishingCompanyID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tpublishingcompany", x => x.nPublishingCompanyID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tauthor");

            migrationBuilder.DropTable(
                name: "tbook");

            migrationBuilder.DropTable(
                name: "tpublishingcompany");
        }
    }
}
