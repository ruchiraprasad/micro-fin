using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fin.Infrastructure.Migrations
{
    public partial class CreateDB_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "LoanDetail");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "Customer");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "User",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "LoanDetail",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Loan",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Customer",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "LoanDetail");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Customer");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "User",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "LoanDetail",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "Loan",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "Customer",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
