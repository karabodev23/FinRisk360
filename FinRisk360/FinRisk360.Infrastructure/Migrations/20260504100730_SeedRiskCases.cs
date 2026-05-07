using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinRisk360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRiskCases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RiskCases",
                columns: new[] { "Id", "CaseType", "CreatedAt", "CustomerName", "Description", "RiskLevel", "Status" },
                values: new object[,]
                {
                    { 100, "Fraud Investigation", new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thabo Mokoena", "Suspicious card transactions detected.", "High", "Open" },
                    { 200, "AML Review", new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aisha Naidoo", "Large deposits require anti-money laundering review.", "Medium", "In Progress" },
                    { 300, "KYC Verification", new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lerato Dlamini", "Customer profile verification completed.", "Low", "Closed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 300);
        }
    }
}
