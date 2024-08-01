using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SingleRepoPokemonApi.Migrations
{
    /// <inheritdoc />
    public partial class restructuredatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAttribute_Pokemon_PokemonId",
                table: "PokemonAttribute");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAttribute_Pokemon_PokemonId",
                table: "PokemonAttribute",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAttribute_Pokemon_PokemonId",
                table: "PokemonAttribute");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAttribute_Pokemon_PokemonId",
                table: "PokemonAttribute",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
