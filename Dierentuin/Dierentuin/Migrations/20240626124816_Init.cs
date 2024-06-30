using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dierentuin.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "enclosures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Climate = table.Column<int>(type: "INTEGER", nullable: false),
                    Habitat = table.Column<int>(type: "INTEGER", nullable: false),
                    SecurityLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enclosures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Species = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    categories_id = table.Column<int>(type: "INTEGER", nullable: true),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Dietary = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityPattern = table.Column<int>(type: "INTEGER", nullable: false),
                    Prey = table.Column<int>(type: "INTEGER", nullable: true),
                    enclosures_id = table.Column<int>(type: "INTEGER", nullable: true),
                    spaceRequirement = table.Column<double>(type: "REAL", nullable: false),
                    SecurityRequirement = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_animals_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_animals_enclosures_enclosures_id",
                        column: x => x.enclosures_id,
                        principalTable: "enclosures",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Odit eum exercitationem.", "Aquatic Animals" },
                    { 2, "Itaque labore esse.", "Aquatic Animals" },
                    { 3, "Facilis quaerat repellat.", "Bats" },
                    { 4, "In dolor reprehenderit.", "Ungulates" },
                    { 5, "Et nihil beatae.", "Insects" },
                    { 6, "Voluptatem dolorum tempora.", "Mustelids" },
                    { 7, "Quaerat eius consectetur.", "Reptiles" },
                    { 8, "Nam facere hic.", "Bats" },
                    { 9, "Enim harum ullam.", "Rodents" },
                    { 10, "Deleniti delectus beatae.", "Mammals" }
                });

            migrationBuilder.InsertData(
                table: "enclosures",
                columns: new[] { "Id", "Climate", "Habitat", "Name", "SecurityLevel", "Size" },
                values: new object[,]
                {
                    { 1, 2, 1, "Territory", 1, 14.771890186205578 },
                    { 2, 1, 3, "Pool", 2, 45.078704872757662 },
                    { 3, 0, 2, "Aviary", 0, 25.972496293230325 },
                    { 4, 2, 3, "Habitat", 0, 48.447796497417322 },
                    { 5, 0, 2, "Paddock", 0, 23.876876249455776 },
                    { 6, 2, 2, "Territory", 1, 46.042498711809728 },
                    { 7, 1, 0, "Grove", 0, 30.081723610151997 },
                    { 8, 0, 1, "House", 2, 52.208789955654453 },
                    { 9, 1, 0, "Cave", 1, 83.003769239213298 },
                    { 10, 0, 2, "Cave", 0, 2.1855974660035917 }
                });

            migrationBuilder.InsertData(
                table: "animals",
                columns: new[] { "Id", "ActivityPattern", "categories_id", "Dietary", "enclosures_id", "Name", "Prey", "SecurityRequirement", "Size", "spaceRequirement", "Species" },
                values: new object[,]
                {
                    { 1, 0, 6, 4, 1, "Tremayne", 6, 1, 3, 76.005636193022866, "Aquatic Animals" },
                    { 2, 2, 5, 3, 5, "Jeffry", 3, 0, 3, 43.983474680949378, "Ungulates" },
                    { 3, 0, 8, 1, 5, "Miller", 2, 2, 1, 72.21702100520362, "Insects" },
                    { 4, 1, 9, 2, 8, "Arden", 6, 1, 1, 18.966558877529501, "Birds" },
                    { 5, 1, 5, 1, 9, "Tavares", 5, 0, 1, 47.826318734103445, "Bovines" },
                    { 6, 2, 6, 1, 7, "Zula", 9, 1, 5, 17.620696395763002, "Pachyderms" },
                    { 7, 2, 2, 4, 1, "Juliet", 8, 0, 3, 66.017261503547758, "Arachnids" },
                    { 8, 2, 3, 2, 3, "Jevon", 2, 0, 5, 5.710742433160922, "Pachyderms" },
                    { 9, 2, 1, 2, 3, "Jaquan", 3, 1, 1, 95.890223980597995, "Arachnids" },
                    { 10, 1, 4, 3, 6, "Jermey", 1, 1, 1, 69.563248388446652, "Canines" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_animals_categories_id",
                table: "animals",
                column: "categories_id");

            migrationBuilder.CreateIndex(
                name: "IX_animals_enclosures_id",
                table: "animals",
                column: "enclosures_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animals");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "enclosures");
        }
    }
}
