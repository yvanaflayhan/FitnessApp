using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerRequestGymInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "TrainerRequests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherGymName",
                table: "TrainerRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorksIndependently",
                table: "TrainerRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRequests_GymId",
                table: "TrainerRequests",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerRequests_Gyms_GymId",
                table: "TrainerRequests",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerRequests_Gyms_GymId",
                table: "TrainerRequests");

            migrationBuilder.DropIndex(
                name: "IX_TrainerRequests_GymId",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "OtherGymName",
                table: "TrainerRequests");

            migrationBuilder.DropColumn(
                name: "WorksIndependently",
                table: "TrainerRequests");
        }
    }
}
