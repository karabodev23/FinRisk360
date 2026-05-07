using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinRisk360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RiskScore",
                table: "RiskCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 1,
                column: "RiskScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 2,
                column: "RiskScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 3,
                column: "RiskScore",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RiskScore",
                table: "RiskCases");
        }
    }
}
