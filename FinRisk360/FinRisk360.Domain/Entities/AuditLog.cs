namespace FinRisk360.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }

    public int RiskCaseId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string PerformedBy { get; set; } = string.Empty;

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;
}