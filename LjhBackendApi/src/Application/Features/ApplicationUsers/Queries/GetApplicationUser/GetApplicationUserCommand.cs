using LjhBackendApi.Application.Common.Interfaces;
using LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetApplicationUser;

namespace LjhBackendApi.Application.ApplicationUsers.Queries.GetApplicationUser;

public record GetApplicationUserQuery : IRequest<ApplicationUserDto>
{
}

public class GetApplicationUserQueryValidator : AbstractValidator<GetApplicationUserQuery>
{
    public GetApplicationUserQueryValidator()
    {
    }
}

public class GetApplicationUserQueryHandler : IRequestHandler<GetApplicationUserQuery, ApplicationUserDto>
{
    private readonly IApplicationDbContext _context;

    public GetApplicationUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUserDto> Handle(GetApplicationUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
