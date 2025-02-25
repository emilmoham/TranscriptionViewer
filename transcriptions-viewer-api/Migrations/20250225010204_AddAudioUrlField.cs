using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranscriptionsViewerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAudioUrlField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioUrl",
                table: "Meetings",
                type: "character varying(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioUrl",
                table: "Meetings");
        }
    }
}
