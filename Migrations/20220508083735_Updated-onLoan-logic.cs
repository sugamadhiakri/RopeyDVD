using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RopeyDVD.Migrations
{
    public partial class UpdatedonLoanlogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReturned",
                table: "Loan",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<bool>(
                name: "IsLoaned",
                table: "DVDCopy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Actor",
                columns: new[] { "ActorId", "Firstname", "Lastname" },
                values: new object[,]
                {
                    { 1, "Rajesh", "Hamal" },
                    { 2, "Salman", "Khan" },
                    { 3, "Anamol", "Kc" },
                    { 4, "David", "Rimal" },
                    { 5, "Pragya", "Bhandari" },
                    { 6, "Miraj", "Pokhrel" },
                    { 7, "Sujan", "Giri" },
                    { 8, "Rani", "Poudel" },
                    { 9, "Gopal", "Adhikari" },
                    { 10, "Anurag", "Dhungana" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "IsLoaned",
                table: "DVDCopy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReturned",
                table: "Loan",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
