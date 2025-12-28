using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeutralContainer.Migrations
{
    /// <inheritdoc />
    public partial class AddResponseAgreementToPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedFeedbackModes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvoidanceModes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContextText",
                table: "Posts",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomRulesText",
                table: "Posts",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModerationLevel",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SensitivityFlags",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisibilityPolicy",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedFeedbackModes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AvoidanceModes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ContextText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CustomRulesText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ModerationLevel",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SensitivityFlags",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VisibilityPolicy",
                table: "Posts");
        }
    }
}
