using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    public partial class StoreWithImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Store_StoreId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_AspNetUsers_OwnerId",
                table: "Store");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_Categories_CategoryId",
                table: "Store");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreImage_Store_StoreId",
                table: "StoreImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreImage",
                table: "StoreImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Store",
                table: "Store");

            migrationBuilder.RenameTable(
                name: "StoreImage",
                newName: "StoreImages");

            migrationBuilder.RenameTable(
                name: "Store",
                newName: "Stores");

            migrationBuilder.RenameIndex(
                name: "IX_StoreImage_StoreId",
                table: "StoreImages",
                newName: "IX_StoreImages_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Store_OwnerId",
                table: "Stores",
                newName: "IX_Stores_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Store_CategoryId",
                table: "Stores",
                newName: "IX_Stores_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Voen",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Voen",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Weekend",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "atWeek",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreImages",
                table: "StoreImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stores_StoreId",
                table: "AspNetUsers",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreImages_Stores_StoreId",
                table: "StoreImages",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AspNetUsers_OwnerId",
                table: "Stores",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Categories_CategoryId",
                table: "Stores",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stores_StoreId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreImages_Stores_StoreId",
                table: "StoreImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AspNetUsers_OwnerId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Categories_CategoryId",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreImages",
                table: "StoreImages");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Voen",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Voen",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Weekend",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "atWeek",
                table: "Stores");

            migrationBuilder.RenameTable(
                name: "Stores",
                newName: "Store");

            migrationBuilder.RenameTable(
                name: "StoreImages",
                newName: "StoreImage");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_OwnerId",
                table: "Store",
                newName: "IX_Store_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_CategoryId",
                table: "Store",
                newName: "IX_Store_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreImages_StoreId",
                table: "StoreImage",
                newName: "IX_StoreImage_StoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Store",
                table: "Store",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreImage",
                table: "StoreImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Store_StoreId",
                table: "AspNetUsers",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_AspNetUsers_OwnerId",
                table: "Store",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Categories_CategoryId",
                table: "Store",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreImage_Store_StoreId",
                table: "StoreImage",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
