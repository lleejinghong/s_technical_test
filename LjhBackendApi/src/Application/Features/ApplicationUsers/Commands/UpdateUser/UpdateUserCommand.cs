using LjhBackendApi.Application.Common.Models;
using LjhBackendApi.Application.Contracts;
using LjhBackendApi.Application.Features.ApplicationUsers.Queries;

namespace LjhBackendApi.Application.Features.ApplicationUsers.Commands.UpdateUser;
public class UpdateUserRequest: IRequest<Result>
{
    public UpdateUserDto UpdateUserDto { get; set; }
}

public class UpdateUserHandler: IRequestHandler<UpdateUserRequest, Result>
{
    private readonly IApplicationUsersRepository _applicationUsersRepository;

    public UpdateUserHandler(IApplicationUsersRepository applicationUsersRepository)
    {
        _applicationUsersRepository = applicationUsersRepository;
    }

    public async Task<Result> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _applicationUsersRepository.GetUser(request.UpdateUserDto.Email);
            user.FirstName = request.UpdateUserDto.FirstName;
            user.LastName = request.UpdateUserDto.LastName;
            user.Email = request.UpdateUserDto.Email;
            user.PhoneNumber = request.UpdateUserDto.PhoneNumber;
            user.ZipCode = request.UpdateUserDto.ZipCode;

            var updatedUser = await _applicationUsersRepository.UpdateUser(user);
            return Result.Success(updatedUser);
        }
        catch (Exception ex)
        {
            return Result.Failure([ex.Message]);
        }

    }
}
