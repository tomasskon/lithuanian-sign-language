using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignRecognition.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSignsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Signs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SignName = table.Column<string>(type: "text", nullable: false),
                    SignVideoUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Signs");
        }
    }
}
