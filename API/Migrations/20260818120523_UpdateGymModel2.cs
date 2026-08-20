using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGymModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Gyms",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Gyms",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ClosingHours",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MounthlyFee",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningHours",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Gyms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialMedia",
                table: "Gyms",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHours",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "MounthlyFee",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "SocialMedia",
                table: "Gyms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Gyms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Gyms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
