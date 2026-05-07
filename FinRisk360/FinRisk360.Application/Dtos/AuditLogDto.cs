namespace FinRisk360.Application.Dtos;

public class AuditLogDto
{
    public int Id { get; set; }

    public int RiskCaseId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string PerformedBy { get; set; } = string.Empty;

    public DateTime PerformedAt { get; set; }

    public string Notes { get; set; } = string.Empty;
}