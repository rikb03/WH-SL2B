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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "enclosures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Climate = table.Column<int>(type: "int", nullable: false),
                    Habitat = table.Column<int>(type: "int", nullable: false),
                    SecurityLevel = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enclosures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    categories_id = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Dietary = table.Column<int>(type: "int", nullable: false),
                    ActivityPattern = table.Column<int>(type: "int", nullable: false),
                    Prey = table.Column<int>(type: "int", nullable: true),
                    enclosures_id = table.Column<int>(type: "int", nullable: true),
                    spaceRequirement = table.Column<double>(type: "float", nullable: false),
                    SecurityRequirement = table.Column<int>(type: "int", nullable: false)
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
                    { 1, "Nulla vel ipsum.", "Birds" },
                    { 2, "Molestiae officia est.", "Mammals" },
                    { 3, "Est alias iure.", "Cetaceans" },
                    { 4, "Tempora cumque mollitia.", "Marsupials" },
                    { 5, "Odit excepturi sit.", "Mustelids" },
                    { 6, "Suscipit cumque adipisci.", "Rodents" },
                    { 7, "Numquam enim aliquid.", "Marsupials" },
                    { 8, "Incidunt quisquam nulla.", "Cetaceans" },
                    { 9, "Reiciendis debitis impedit.", "Bovines" },
                    { 10, "Et ut accusantium.", "Insects" }
                });

            migrationBuilder.InsertData(
                table: "enclosures",
                columns: new[] { "Id", "Climate", "Habitat", "Name", "SecurityLevel", "Size" },
                values: new object[,]
                {
                    { 1, 0, 0, "Reserve", 1, 27.522662727696428 },
                    { 2, 2, 3, "Dome", 0, 83.072825068062414 },
                    { 3, 0, 1, "House", 0, 96.36872095566504 },
                    { 4, 0, 0, "Habitat", 1, 74.541575605704395 },
                    { 5, 1, 1, "House", 2, 44.015445261759993 },
                    { 6, 1, 1, "Kooi", 2, 35.207897451587122 },
                    { 7, 2, 1, "Pavilion", 1, 6.5657923927528081 },
                    { 8, 0, 1, "Enclosure", 0, 43.350064590647868 },
                    { 9, 1, 1, "Outback", 0, 11.322536233163754 },
                    { 10, 2, 3, "Island", 2, 21.247454788938654 }
                });

            migrationBuilder.InsertData(
                table: "animals",
                columns: new[] { "Id", "ActivityPattern", "categories_id", "Dietary", "enclosures_id", "Name", "Prey", "SecurityRequirement", "Size", "spaceRequirement", "Species" },
                values: new object[,]
                {
                    { 1, 0, 4, 1, 9, "Fiona", 4, 0, 4, 52.609020481924709, "Pachyderms" },
                    { 2, 2, 9, 0, 1, "Nicklaus", 7, 1, 0, 2.9302837644599737, "Pachyderms" },
                    { 3, 0, 5, 0, 7, "Adolphus", 9, 0, 3, 41.734799958790504, "Birds" },
                    { 4, 0, 2, 4, 10, "Amaya", 2, 1, 1, 6.6179048408281327, "Ungulates" },
                    { 5, 2, 3, 4, 7, "Anabel", 6, 0, 3, 4.9489435151673806, "Pachyderms" },
                    { 6, 1, 5, 2, 5, "Enrique", 4, 0, 2, 81.141339057972431, "Reptiles" },
                    { 7, 2, 4, 1, 1, "Collin", 7, 2, 2, 10.491983032470461, "Primates" },
                    { 8, 2, 6, 0, 8, "Filiberto", 9, 1, 4, 36.987949151280667, "Aquatic Animals" },
                    { 9, 1, 9, 0, 8, "Vilma", 3, 0, 4, 73.350852445513183, "Bats" },
                    { 10, 2, 9, 3, 2, "Muriel", 8, 2, 2, 22.775192582709639, "Crustaceans" }
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
