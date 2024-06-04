using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_MVC.Migrations
{
    public partial class UpdatedCategoryImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryImage_Categories_CategoryId",
                table: "CategoryImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryImage",
                table: "CategoryImage");

            migrationBuilder.RenameTable(
                name: "CategoryImage",
                newName: "CategoryImages");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryImage_CategoryId",
                table: "CategoryImages",
                newName: "IX_CategoryImages_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryImages",
                table: "CategoryImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryImages_Categories_CategoryId",
                table: "CategoryImages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryImages_Categories_CategoryId",
                table: "CategoryImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryImages",
                table: "CategoryImages");

            migrationBuilder.RenameTable(
                name: "CategoryImages",
                newName: "CategoryImage");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryImages_CategoryId",
                table: "CategoryImage",
                newName: "IX_CategoryImage_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryImage",
                table: "CategoryImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryImage_Categories_CategoryId",
                table: "CategoryImage",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
