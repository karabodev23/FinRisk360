namespace FinRisk360.Application.Dtos;

public class RiskCaseDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CaseType { get; set; } = string.Empty;

    public string RiskLevel { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string AssignedTo { get; set; } = string.Empty;

    public int RiskScore { get; set; }

    public int? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}