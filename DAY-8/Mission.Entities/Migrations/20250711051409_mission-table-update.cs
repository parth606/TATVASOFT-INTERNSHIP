using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mission.Entities.Migrations
{
    /// <inheritdoc />
    public partial class missiontableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_City_country_id",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Country_city_id",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "totalsheets",
                table: "Missions",
                newName: "total_sheets");

            migrationBuilder.AlterColumn<int>(
                name: "total_sheets",
                table: "Missions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "mission_availability",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mission_documents",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mission_video_url",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "registration_deadline",
                table: "Missions",
                type: "Date",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_City_city_id",
                table: "Missions",
                column: "city_id",
                principalTable: "City",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Country_country_id",
                table: "Missions",
                column: "country_id",
                principalTable: "Country",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_City_city_id",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Country_country_id",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "mission_availability",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "mission_documents",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "mission_video_url",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "registration_deadline",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "total_sheets",
                table: "Missions",
                newName: "totalsheets");

            migrationBuilder.AlterColumn<int>(
                name: "totalsheets",
                table: "Missions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_City_country_id",
                table: "Missions",
                column: "country_id",
                principalTable: "City",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Country_city_id",
                table: "Missions",
                column: "city_id",
                principalTable: "Country",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
