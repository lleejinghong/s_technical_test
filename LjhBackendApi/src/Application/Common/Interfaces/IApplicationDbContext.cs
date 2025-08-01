using LjhBackendApi.Domain.Entities;

namespace LjhBackendApi.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
