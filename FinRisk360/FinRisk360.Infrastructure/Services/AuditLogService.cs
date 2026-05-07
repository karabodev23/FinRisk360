using FinRisk360.Application.Dtos;
using FinRisk360.Application.Interfaces;
using FinRisk360.Domain.Entities;
using FinRisk360.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinRisk360.Infrastructure.Services;

public class AuditLogService : IAuditLogService
{
    private readonly AppDbContext _context;

    public AuditLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AuditLogDto>> GetAllAsync()
    {
        return await _context.AuditLogs
            .OrderByDescending(x => x.PerformedAt)
            .Select(x => new AuditLogDto
            {
                Id = x.Id,
                RiskCaseId = x.RiskCaseId,
                Action = x.Action,
                PerformedBy = x.PerformedBy,
                PerformedAt = x.PerformedAt,
                Notes = x.Notes
            })
            .ToListAsync();
    }

    public async Task<List<AuditLogDto>> GetByRiskCaseIdAsync(int riskCaseId)
    {
        return await _context.AuditLogs
            .Where(x => x.RiskCaseId == riskCaseId)
            .OrderByDescending(x => x.PerformedAt)
            .Select(x => new AuditLogDto
            {
                Id = x.Id,
                RiskCaseId = x.RiskCaseId,
                Action = x.Action,
                PerformedBy = x.PerformedBy,
                PerformedAt = x.PerformedAt,
                Notes = x.Notes
            })
            .ToListAsync();
    }

    public async Task<AuditLogDto> CreateAsync(AuditLogDto auditLogDto)
    {
        var auditLog = new AuditLog
        {
            RiskCaseId = auditLogDto.RiskCaseId,
            Action = auditLogDto.Action,
            PerformedBy = auditLogDto.PerformedBy,
            PerformedAt = DateTime.UtcNow,
            Notes = auditLogDto.Notes
        };

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();

        return new AuditLogDto
        {
            Id = auditLog.Id,
            RiskCaseId = auditLog.RiskCaseId,
            Action = auditLog.Action,
            PerformedBy = auditLog.PerformedBy,
            PerformedAt = auditLog.PerformedAt,
            Notes = auditLog.Notes
        };
    }
}