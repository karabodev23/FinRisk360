using FinRisk360.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinRisk360.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<RiskCase> RiskCases { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                Name = "Fraud",
                Description = "Handles suspected fraud and suspicious transactions."
            },
            new Department
            {
                Id = 2,
                Name = "AML",
                Description = "Anti-money laundering monitoring and reviews."
            },
            new Department
            {
                Id = 3,
                Name = "KYC",
                Description = "Customer verification and onboarding checks."
            },
            new Department
            {
                Id = 4,
                Name = "Compliance",
                Description = "Regulatory compliance and internal policy reviews."
            },
            new Department
            {
                Id = 5,
                Name = "Retail Banking",
                Description = "Customer account and branch banking cases."
            }
        );

        modelBuilder.Entity<RiskCase>().HasData(
            new RiskCase
            {
                Id = 1,
                CustomerName = "Thabo Mokoena",
                CaseType = "Fraud Investigation",
                RiskLevel = "High",
                Status = "Open",
                Priority = "Urgent",
                AssignedTo = "Karabo Malatji",
                RiskScore = 100,
                DepartmentId = 1,
                Description = "Suspicious card transactions detected.",
                CreatedAt = new DateTime(2026, 5, 4)
            },
            new RiskCase
            {
                Id = 2,
                CustomerName = "Aisha Naidoo",
                CaseType = "AML Review",
                RiskLevel = "Medium",
                Status = "In Progress",
                Priority = "Medium",
                AssignedTo = "Analyst Team",
                RiskScore = 60,
                DepartmentId = 2,
                Description = "Large deposits require anti-money laundering review.",
                CreatedAt = new DateTime(2026, 5, 4)
            },
            new RiskCase
            {
                Id = 3,
                CustomerName = "Lerato Dlamini",
                CaseType = "KYC Verification",
                RiskLevel = "Low",
                Status = "Closed",
                Priority = "Low",
                AssignedTo = "KYC Team",
                RiskScore = 30,
                DepartmentId = 3,
                Description = "Customer profile verification completed.",
                CreatedAt = new DateTime(2026, 5, 4)
            }
        );
    }
}