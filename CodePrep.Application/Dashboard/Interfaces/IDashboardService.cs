using CodePrep.Application.Dashboard.DTOs;

namespace CodePrep.Application.Dashboard.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync(Guid userId);
}