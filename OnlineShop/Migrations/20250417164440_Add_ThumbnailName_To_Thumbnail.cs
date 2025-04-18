using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class Add_ThumbnailName_To_Thumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Thumbnail_ThumbnailId",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailName",
                table: "Thumbnail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "IX_BlogThumbnail_ThumbnailId",
                table: "BlogThumbnail",
                column: "ThumbnailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogThumbnail");

            migrationBuilder.DropColumn(
                name: "ThumbnailName",
                table: "Thumbnail");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Thumbnail_ThumbnailId",
                table: "Blog",
                column: "ThumbnailId",
                principalTable: "Thumbnail",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
