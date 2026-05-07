using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinRisk360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "RiskCases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Handles suspected fraud and suspicious transactions.", "Fraud" },
                    { 2, "Anti-money laundering monitoring and reviews.", "AML" },
                    { 3, "Customer verification and onboarding checks.", "KYC" },
                    { 4, "Regulatory compliance and internal policy reviews.", "Compliance" },
                    { 5, "Customer account and branch banking cases.", "Retail Banking" }
                });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssignedTo", "DepartmentId", "Priority", "RiskScore" },
                values: new object[] { "Karabo Malatji", 1, "Urgent", 100 });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AssignedTo", "DepartmentId", "RiskScore" },
                values: new object[] { "Analyst Team", 2, 60 });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AssignedTo", "DepartmentId", "Priority", "RiskScore" },
                values: new object[] { "KYC Team", 3, "Low", 30 });

            migrationBuilder.CreateIndex(
                name: "IX_RiskCases_DepartmentId",
                table: "RiskCases",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskCases_Departments_DepartmentId",
                table: "RiskCases",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskCases_Departments_DepartmentId",
                table: "RiskCases");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_RiskCases_DepartmentId",
                table: "RiskCases");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "RiskCases");

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssignedTo", "Priority", "RiskScore" },
                values: new object[] { "", "Medium", 0 });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AssignedTo", "RiskScore" },
                values: new object[] { "", 0 });

            migrationBuilder.UpdateData(
                table: "RiskCases",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AssignedTo", "Priority", "RiskScore" },
                values: new object[] { "", "Medium", 0 });
        }
    }
}
