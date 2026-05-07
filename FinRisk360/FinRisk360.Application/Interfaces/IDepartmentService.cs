using FinRisk360.Application.Dtos;

namespace FinRisk360.Application.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllAsync();

    Task<DepartmentDto?> GetByIdAsync(int id);
}