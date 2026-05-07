using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinRisk360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityAndAssignedTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "RiskCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "RiskCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskCaseId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssignedTo", "Priority" },
                values: new object[] { "", "Medium" });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AssignedTo", "Priority" },
                values: new object[] { "", "Medium" });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AssignedTo", "Priority" },
                values: new object[] { "", "Medium" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "RiskCases");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "RiskCases");
        }
    }
}
