using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POST.QUERY.INFRASTRUCTURE.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Post",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DataPosted = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Likes = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Post", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Comment",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Edited = table.Column<bool>(type: "bit", nullable: false),
            //        PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Comment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Comment_Post_PostId",
            //            column: x => x.PostId,
            //            principalTable: "Post",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comment_PostId",
            //    table: "Comment",
            //    column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
