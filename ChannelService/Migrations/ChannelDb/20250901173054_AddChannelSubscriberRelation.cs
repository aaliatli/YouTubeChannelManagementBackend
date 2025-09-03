using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelService.Migrations.ChannelDb
{
    /// <inheritdoc />
    public partial class AddChannelSubscriberRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserLastName",
                table: "ChannelSubscribers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ChannelSubscribers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserLastName",
                table: "ChannelSubscribers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ChannelSubscribers");
        }
    }
}
