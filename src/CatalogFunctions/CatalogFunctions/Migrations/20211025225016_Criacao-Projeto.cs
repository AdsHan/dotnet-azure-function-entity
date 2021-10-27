using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CatalogFunctions.Migrations
{
    public partial class CriacaoProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreateAt = table.Column<DateTime>(nullable: false),
                    DateDeleteAt = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
