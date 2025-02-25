using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranscriptionsViewerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordingAndCaptionKeyFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioUrl",
                table: "Meetings");

            migrationBuilder.AddColumn<string>(
                name: "CaptionsKey",
                table: "Meetings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecordingKey",
                table: "Meetings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaptionsKey",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "RecordingKey",
                table: "Meetings");

            migrationBuilder.AddColumn<string>(
                name: "AudioUrl",
                table: "Meetings",
                type: "character varying(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");
        }
    }
}
