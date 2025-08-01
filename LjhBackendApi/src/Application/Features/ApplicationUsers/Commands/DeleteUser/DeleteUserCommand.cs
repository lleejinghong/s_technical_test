﻿using LjhBackendApi.Application.Common.Models;
using LjhBackendApi.Application.Contracts;
using LjhBackendApi.Domain.Entities;
using Microsoft.Rest;

namespace LjhBackendApi.Application.Features.ApplicationUsers.Commands.DeleteUser;

public class DeleteUserRequest : IRequest<Result>
{
    public string Email { get; set; }
}

public class DeleteUserCommand: IRequestHandler<DeleteUserRequest, Result>
{
    private readonly IApplicationUsersRepository _applicationUsersRepository;

    public DeleteUserCommand(
        IApplicationUsersRepository applicationUsersRepository)
    {
        _applicationUsersRepository = applicationUsersRepository;
    }

    public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _applicationUsersRepository.GetUser(request.Email);
            if (user == null) return Result.Failure(["User not found"]);
            var result = await _applicationUsersRepository.DeleteUser(request.Email);
            return Result.Success(result);
        }
        catch (Exception)
        {
            return Result.Failure(["Failed to delete user"]);
        }
    }
}
