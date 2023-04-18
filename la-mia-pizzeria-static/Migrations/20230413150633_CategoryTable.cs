using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Pizzas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoriaId1",
                table: "Pizzas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_CategoriaId1",
                table: "Pizzas",
                column: "CategoriaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Categories_CategoriaId1",
                table: "Pizzas",
                column: "CategoriaId1",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Categories_CategoriaId1",
                table: "Pizzas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_CategoriaId1",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "CategoriaId1",
                table: "Pizzas");
        }
    }
}
