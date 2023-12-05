using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationModule.Migrations
{
    /// <inheritdoc />
    public partial class addhashtofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "HashSalt",
                table: "Files",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashSalt",
                table: "Files");
        }
    }
}
