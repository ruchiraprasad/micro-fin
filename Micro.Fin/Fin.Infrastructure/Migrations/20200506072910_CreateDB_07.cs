using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fin.Infrastructure.Migrations
{
    public partial class CreateDB_07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "LoanDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaidDate",
                table: "LoanDetail",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "LoanDetail",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LatePaid",
                table: "LoanDetail",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "CapitalPaid",
                table: "LoanDetail",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Loan",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapitalPaid",
                table: "LoanDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaidDate",
                table: "LoanDetail",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "LoanDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LatePaid",
                table: "LoanDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterest",
                table: "LoanDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Interest",
                table: "Loan",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
