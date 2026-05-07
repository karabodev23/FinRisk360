using FinRisk360.Application.Dtos;

namespace FinRisk360.Application.Interfaces;

public interface IAuditLogService
{
    Task<List<AuditLogDto>> GetAllAsync();

    Task<List<AuditLogDto>> GetByRiskCaseIdAsync(int riskCaseId);

    Task<AuditLogDto> CreateAsync(AuditLogDto auditLogDto);
}