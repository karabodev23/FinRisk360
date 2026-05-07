using FinRisk360.Application.Dtos;
using FinRisk360.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinRisk360.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RiskCasesController : ControllerBase
{
    private readonly IRiskCaseService _riskCaseService;

    public RiskCasesController(IRiskCaseService riskCaseService)
    {
        _riskCaseService = riskCaseService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Analyst")]
    public async Task<ActionResult<PagedResultDto<RiskCaseDto>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] string? status,
        [FromQuery] string? riskLevel,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var riskCases = await _riskCaseService.GetAllAsync(
            search,
            status,
            riskLevel,
            pageNumber,
            pageSize);

        return Ok(riskCases);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Analyst")]
    public async Task<ActionResult<RiskCaseDto>> GetById(int id)
    {
        var riskCase = await _riskCaseService.GetByIdAsync(id);

        if (riskCase == null)
        {
            return NotFound();
        }

        return Ok(riskCase);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RiskCaseDto>> Create(RiskCaseDto riskCaseDto)
    {
        var createdRiskCase = await _riskCaseService.CreateAsync(riskCaseDto);

        return CreatedAtAction(nameof(GetById), new { id = createdRiskCase.Id }, createdRiskCase);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, RiskCaseDto riskCaseDto)
    {
        var updated = await _riskCaseService.UpdateAsync(id, riskCaseDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _riskCaseService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("dashboard-stats")]
    [Authorize(Roles = "Admin,Analyst")]
    public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
    {
        var stats = await _riskCaseService.GetDashboardStatsAsync();

        return Ok(stats);
    }

    [HttpGet("department-stats")]
    [Authorize(Roles = "Admin,Analyst")]
    public async Task<ActionResult<List<DepartmentStatsDto>>> GetDepartmentStats()
    {
        var stats = await _riskCaseService.GetDepartmentStatsAsync();

        return Ok(stats);
    }
}