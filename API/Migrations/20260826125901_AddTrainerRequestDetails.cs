using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerRequestDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "TrainerRequests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CvUrl",
                table: "TrainerRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "TrainerRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "TrainerRequests",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TrainerRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TrainerRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "TrainerRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "TrainerRequests",
                type: "REAL",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRequests_UserId",
                table: "TrainerRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerRequests_Users_UserId",
                table: "TrainerRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerRequests_Users_UserId",
                table: "TrainerRequests");

            migrationBuilder.DropIndex(
                name: "IX_TrainerRequests_UserId",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "CvUrl",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TrainerRequests");
        }
    }
}
