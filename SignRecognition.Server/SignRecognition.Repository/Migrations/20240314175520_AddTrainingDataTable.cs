using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignRecognition.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingData_Signs_SignId",
                        column: x => x.SignId,
                        principalTable: "Signs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingData_SignId",
                table: "TrainingData",
                column: "SignId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingData_UserId",
                table: "TrainingData",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingData");
        }
    }
}
