using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationModule.Migrations
{
    /// <inheritdoc />
    public partial class addfilehashmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileHashes",
                columns: table => new
                {
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileHashes", x => x.Path);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileHashes");
        }
    }
}
