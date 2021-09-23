using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndAlbergue.Migrations
{
    public partial class PetShopCRUD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "petShop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    SizeOfPet = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Stock = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_petShop", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "petShop");
        }
    }
}
