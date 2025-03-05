using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace transcriptionsviewerapi.Migrations
{
    /// <inheritdoc />
    public partial class SeparateTitleAndSummaryVector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchVector",
                table: "Meetings",
                newName: "TitleSearchVector");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_SearchVector",
                table: "Meetings",
                newName: "IX_Meetings_TitleSearchVector");

            migrationBuilder.AlterColumn<NpgsqlTsVector>(
                name: "TitleSearchVector",
                table: "Meetings",
                type: "tsvector",
                nullable: false,
                oldClrType: typeof(NpgsqlTsVector),
                oldType: "tsvector")
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title" })
                .OldAnnotation("Npgsql:TsVectorConfig", "english")
                .OldAnnotation("Npgsql:TsVectorProperties", new[] { "Title", "Summary" });

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SummarySearchVector",
                table: "Meetings",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Summary" });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SummarySearchVector",
                table: "Meetings",
                column: "SummarySearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meetings_SummarySearchVector",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "SummarySearchVector",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "TitleSearchVector",
                table: "Meetings",
                newName: "SearchVector");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_TitleSearchVector",
                table: "Meetings",
                newName: "IX_Meetings_SearchVector");

            migrationBuilder.AlterColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Meetings",
                type: "tsvector",
                nullable: false,
                oldClrType: typeof(NpgsqlTsVector),
                oldType: "tsvector")
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Summary" })
                .OldAnnotation("Npgsql:TsVectorConfig", "english")
                .OldAnnotation("Npgsql:TsVectorProperties", new[] { "Title" });
        }
    }
}
