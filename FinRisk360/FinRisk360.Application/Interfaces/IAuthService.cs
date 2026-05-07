using FinRisk360.Application.Dtos;

namespace FinRisk360.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);

    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}