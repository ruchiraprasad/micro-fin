using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fin.Infrastructure.Migrations
{
    public partial class CreateDB_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "User",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    TimeSpan = table.Column<TimeSpan>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Comment = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    TimeSpan = table.Column<TimeSpan>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    InitialLoanAmount = table.Column<decimal>(nullable: false),
                    DateGranted = table.Column<DateTime>(nullable: false),
                    PeriodMonths = table.Column<int>(nullable: false),
                    Interest = table.Column<int>(nullable: false),
                    Security = table.Column<string>(nullable: true),
                    PropertyValue = table.Column<decimal>(nullable: false),
                    CapitalOutstanding = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    TimeSpan = table.Column<TimeSpan>(nullable: false),
                    LoanId = table.Column<int>(nullable: false),
                    Month = table.Column<DateTime>(nullable: false),
                    MonthlyInterest = table.Column<decimal>(nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    LatePaid = table.Column<decimal>(nullable: false),
                    PaidDate = table.Column<DateTime>(nullable: false),
                    TotalInterest = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    InterestType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanDetail_Loan_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CustomerId",
                table: "Loan",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetail_LoanId",
                table: "LoanDetail",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDetail");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "User");
        }
    }
}
