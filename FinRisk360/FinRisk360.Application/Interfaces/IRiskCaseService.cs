using FinRisk360.Application.Dtos;

namespace FinRisk360.Application.Interfaces;

public interface IRiskCaseService
{
    Task<PagedResultDto<RiskCaseDto>> GetAllAsync(
        string? search,
        string? status,
        string? riskLevel,
        int pageNumber,
        int pageSize);

    Task<RiskCaseDto?> GetByIdAsync(int id);

    Task<RiskCaseDto> CreateAsync(RiskCaseDto riskCaseDto);

    Task<bool> UpdateAsync(int id, RiskCaseDto riskCaseDto);

    Task<bool> DeleteAsync(int id);

    Task<DashboardStatsDto> GetDashboardStatsAsync();

    Task<List<DepartmentStatsDto>> GetDepartmentStatsAsync();
}