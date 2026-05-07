using FinRisk360.Application.Dtos;
using FinRisk360.Application.Interfaces;
using FinRisk360.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinRisk360.Infrastructure.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        return await _context.Departments
            .OrderBy(x => x.Name)
            .Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
        {
            return null;
        }

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }
}