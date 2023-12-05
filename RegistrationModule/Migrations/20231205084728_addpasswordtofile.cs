using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationModule.Migrations
{
    /// <inheritdoc />
    public partial class addpasswordtofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Files");
        }
    }
}
