using FinRisk360.Application.Dtos;
using FinRisk360.Application.Interfaces;
using FinRisk360.Domain.Entities;
using FinRisk360.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinRisk360.Infrastructure.Services;

public class RiskCaseService : IRiskCaseService
{
    private readonly AppDbContext _context;

    public RiskCaseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResultDto<RiskCaseDto>> GetAllAsync(
        string? search,
        string? status,
        string? riskLevel,
        int pageNumber,
        int pageSize)
    {
        var query = _context.RiskCases
            .Include(x => x.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.CustomerName.Contains(search) ||
                x.CaseType.Contains(search) ||
                x.Description.Contains(search) ||
                x.AssignedTo.Contains(search) ||
                (x.Department != null && x.Department.Name.Contains(search)));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(x => x.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(riskLevel))
        {
            query = query.Where(x => x.RiskLevel == riskLevel);
        }

        var totalRecords = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new RiskCaseDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                CaseType = x.CaseType,
                RiskLevel = x.RiskLevel,
                Status = x.Status,
                Priority = x.Priority,
                AssignedTo = x.AssignedTo,
                RiskScore = x.RiskScore,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department != null ? x.Department.Name : null,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return new PagedResultDto<RiskCaseDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }

    public async Task<RiskCaseDto?> GetByIdAsync(int id)
    {
        var riskCase = await _context.RiskCases
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (riskCase == null)
        {
            return null;
        }

        return new RiskCaseDto
        {
            Id = riskCase.Id,
            CustomerName = riskCase.CustomerName,
            CaseType = riskCase.CaseType,
            RiskLevel = riskCase.RiskLevel,
            Status = riskCase.Status,
            Priority = riskCase.Priority,
            AssignedTo = riskCase.AssignedTo,
            RiskScore = riskCase.RiskScore,
            DepartmentId = riskCase.DepartmentId,
            DepartmentName = riskCase.Department?.Name,
            Description = riskCase.Description,
            CreatedAt = riskCase.CreatedAt
        };
    }

    public async Task<RiskCaseDto> CreateAsync(RiskCaseDto riskCaseDto)
    {
        var riskCase = new RiskCase
        {
            CustomerName = riskCaseDto.CustomerName,
            CaseType = riskCaseDto.CaseType,
            RiskLevel = riskCaseDto.RiskLevel,
            Status = riskCaseDto.Status,
            Priority = riskCaseDto.Priority,
            AssignedTo = riskCaseDto.AssignedTo,
            RiskScore = CalculateRiskScore(riskCaseDto.RiskLevel, riskCaseDto.Priority),
            DepartmentId = riskCaseDto.DepartmentId,
            Description = riskCaseDto.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.RiskCases.Add(riskCase);
        await _context.SaveChangesAsync();

        await AddAuditLogAsync(
            riskCase.Id,
            "Created",
            $"Risk case created for {riskCase.CustomerName}.");

        var departmentName = await _context.Departments
            .Where(x => x.Id == riskCase.DepartmentId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync();

        return new RiskCaseDto
        {
            Id = riskCase.Id,
            CustomerName = riskCase.CustomerName,
            CaseType = riskCase.CaseType,
            RiskLevel = riskCase.RiskLevel,
            Status = riskCase.Status,
            Priority = riskCase.Priority,
            AssignedTo = riskCase.AssignedTo,
            RiskScore = riskCase.RiskScore,
            DepartmentId = riskCase.DepartmentId,
            DepartmentName = departmentName,
            Description = riskCase.Description,
            CreatedAt = riskCase.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, RiskCaseDto riskCaseDto)
    {
        var riskCase = await _context.RiskCases.FindAsync(id);

        if (riskCase == null)
        {
            return false;
        }

        riskCase.CustomerName = riskCaseDto.CustomerName;
        riskCase.CaseType = riskCaseDto.CaseType;
        riskCase.RiskLevel = riskCaseDto.RiskLevel;
        riskCase.Status = riskCaseDto.Status;
        riskCase.Priority = riskCaseDto.Priority;
        riskCase.AssignedTo = riskCaseDto.AssignedTo;
        riskCase.DepartmentId = riskCaseDto.DepartmentId;
        riskCase.Description = riskCaseDto.Description;
        riskCase.RiskScore = CalculateRiskScore(riskCase.RiskLevel, riskCase.Priority);

        await _context.SaveChangesAsync();

        await AddAuditLogAsync(
            riskCase.Id,
            "Updated",
            $"Risk case updated for {riskCase.CustomerName}.");

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var riskCase = await _context.RiskCases.FindAsync(id);

        if (riskCase == null)
        {
            return false;
        }

        await AddAuditLogAsync(
            riskCase.Id,
            "Deleted",
            $"Risk case deleted for {riskCase.CustomerName}.");

        _context.RiskCases.Remove(riskCase);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        return new DashboardStatsDto
        {
            TotalCases = await _context.RiskCases.CountAsync(),
            OpenCases = await _context.RiskCases.CountAsync(x => x.Status == "Open"),
            HighRiskCases = await _context.RiskCases.CountAsync(x => x.RiskLevel == "High"),
            ClosedCases = await _context.RiskCases.CountAsync(x => x.Status == "Closed")
        };
    }

    private int CalculateRiskScore(string riskLevel, string priority)
    {
        var score = riskLevel switch
        {
            "Low" => 30,
            "Medium" => 60,
            "High" => 90,
            _ => 50
        };

        if (priority == "Urgent")
        {
            score += 10;
        }

        return score;
    }

    private async Task AddAuditLogAsync(int riskCaseId, string action, string notes)
    {
        var auditLog = new AuditLog
        {
            RiskCaseId = riskCaseId,
            Action = action,
            PerformedBy = "System",
            PerformedAt = DateTime.UtcNow,
            Notes = notes
        };

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DepartmentStatsDto>> GetDepartmentStatsAsync()
    {
        return await _context.RiskCases
            .Include(x => x.Department)
            .GroupBy(x => x.Department != null ? x.Department.Name : "Unassigned")
            .Select(g => new DepartmentStatsDto
            {
                DepartmentName = g.Key,
                TotalCases = g.Count()
            })
            .OrderByDescending(x => x.TotalCases)
            .ToListAsync();
    }
}