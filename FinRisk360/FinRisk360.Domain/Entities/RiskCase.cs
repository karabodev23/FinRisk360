namespace FinRisk360.Domain.Entities;

public class RiskCase
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CaseType { get; set; } = string.Empty;

    public string RiskLevel { get; set; } = string.Empty;

    public string Status { get; set; } = "Open";

    public string Priority { get; set; } = "Medium";

    public string AssignedTo { get; set; } = string.Empty;

    public int RiskScore { get; set; }

    public int? DepartmentId { get; set; }

    public Department? Department { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}