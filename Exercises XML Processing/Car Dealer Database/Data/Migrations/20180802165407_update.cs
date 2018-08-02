using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Suppliers_Supplier_id",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "Supplier_id",
                table: "Parts",
                newName: "Supplier_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_Supplier_id",
                table: "Parts",
                newName: "IX_Parts_Supplier_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Supplier_Id",
                table: "Parts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Suppliers_Supplier_Id",
                table: "Parts",
                column: "Supplier_Id",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Suppliers_Supplier_Id",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "Supplier_Id",
                table: "Parts",
                newName: "Supplier_id");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_Supplier_Id",
                table: "Parts",
                newName: "IX_Parts_Supplier_id");

            migrationBuilder.AlterColumn<int>(
                name: "Supplier_id",
                table: "Parts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Suppliers_Supplier_id",
                table: "Parts",
                column: "Supplier_id",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
