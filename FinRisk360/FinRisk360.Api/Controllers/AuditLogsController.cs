using FinRisk360.Application.Dtos;
using FinRisk360.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinRisk360.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AuditLogsController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    public AuditLogsController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuditLogDto>>> GetAll()
    {
        var auditLogs = await _auditLogService.GetAllAsync();

        return Ok(auditLogs);
    }

    [HttpGet("by-risk-case/{riskCaseId}")]
    public async Task<ActionResult<List<AuditLogDto>>> GetByRiskCaseId(int riskCaseId)
    {
        var auditLogs = await _auditLogService.GetByRiskCaseIdAsync(riskCaseId);

        return Ok(auditLogs);
    }

    [HttpPost]
    public async Task<ActionResult<AuditLogDto>> Create(AuditLogDto auditLogDto)
    {
        var createdAuditLog = await _auditLogService.CreateAsync(auditLogDto);

        return Ok(createdAuditLog);
    }
}