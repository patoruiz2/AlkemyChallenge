using Microsoft.EntityFrameworkCore.Migrations;

namespace AlkemyChallenge.Migrations
{
    public partial class ReworkGenreMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieAndSeries_Genres_GenreId",
                table: "MovieAndSeries");

            migrationBuilder.DropIndex(
                name: "IX_MovieAndSeries_GenreId",
                table: "MovieAndSeries");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "MovieAndSeries");

            migrationBuilder.CreateTable(
                name: "Movie_Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    MovieSerieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Genre", x => new { x.GenreId, x.MovieSerieId });
                    table.ForeignKey(
                        name: "FK_Movie_Genre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movie_Genre_MovieAndSeries_MovieSerieId",
                        column: x => x.MovieSerieId,
                        principalTable: "MovieAndSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Genre_MovieSerieId",
                table: "Movie_Genre",
                column: "MovieSerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie_Genre");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "MovieAndSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieAndSeries_GenreId",
                table: "MovieAndSeries",
                column: "GenreId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieAndSeries_Genres_GenreId",
                table: "MovieAndSeries",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
