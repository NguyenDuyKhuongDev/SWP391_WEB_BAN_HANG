using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class Update_Blog_Thumbnail_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogThumbnail");

            migrationBuilder.DropIndex(
                name: "IX_Blog_ThumbnailId",
                table: "Blog");

            migrationBuilder.AlterColumn<string>(
                name: "ThumbnailName",
                table: "Thumbnail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BlogCategory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(100)",
                oldFixedLength: true,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThumbnailId",
                table: "Blog",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_ThumbnailId",
                table: "Blog",
                column: "ThumbnailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Thumbnail_ThumbnailId",
                table: "Blog",
                column: "ThumbnailId",
                principalTable: "Thumbnail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Thumbnail_ThumbnailId",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_ThumbnailId",
                table: "Blog");

            migrationBuilder.AlterColumn<string>(
                name: "ThumbnailName",
                table: "Thumbnail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "(N'')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BlogCategory",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThumbnailId",
                table: "Blog",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");

            migrationBuilder.CreateTable(
                name: "BlogThumbnail",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogThumbnail", x => new { x.BlogId, x.ThumbnailId });
                    table.ForeignKey(
                        name: "FK_BlogThumbnail_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogThumbnail_Thumbnail_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "Thumbnail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_ThumbnailId",
                table: "Blog",
                column: "ThumbnailId",
                unique: true,
                filter: "[ThumbnailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogThumbnail_ThumbnailId",
                table: "BlogThumbnail",
                column: "ThumbnailId");
        }
    }
}
