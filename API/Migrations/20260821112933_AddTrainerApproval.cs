using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Users_userId",
                table: "Trainers");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Trainers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IsAproved",
                table: "Trainers",
                newName: "IsApproved");

            migrationBuilder.RenameIndex(
                name: "IX_Trainers_userId",
                table: "Trainers",
                newName: "IX_Trainers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Users_UserId",
                table: "Trainers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Users_UserId",
                table: "Trainers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Trainers",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "Trainers",
                newName: "IsAproved");

            migrationBuilder.RenameIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers",
                newName: "IX_Trainers_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Users_userId",
                table: "Trainers",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
