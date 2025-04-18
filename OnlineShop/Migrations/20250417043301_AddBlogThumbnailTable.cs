using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogThumbnailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Blog");

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Blog",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumnailUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Id);
                });

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
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Thumbnail_ThumbnailId",
                table: "Blog");

            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropIndex(
                name: "IX_Blog_ThumbnailId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
