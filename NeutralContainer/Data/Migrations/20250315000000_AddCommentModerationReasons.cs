using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeutralContainer.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentModerationReasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModerationReasonsJson",
                table: "Comments",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationReasonsJson",
                table: "Comments");
        }
    }
}
