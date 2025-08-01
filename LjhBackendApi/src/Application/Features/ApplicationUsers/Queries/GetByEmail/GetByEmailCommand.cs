﻿using LjhBackendApi.Application.Common.Interfaces;
using LjhBackendApi.Application.Common.Interfaces;
using LjhBackendApi.Application.Common.Models;
using LjhBackendApi.Application.Contracts;
using LjhBackendApi.Domain.Entities;

namespace LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetByEmail;

public class GetByEmailRequest : IRequest<Result>
{
    public string Email { get; set; }
}

public class GetByEmailCommand : IRequestHandler<GetByEmailRequest, Result>
{
    private readonly IApplicationUsersRepository _applicationUsersRepository;
    //private readonly IBlobService _blobService; 
    private readonly IMapper _mapper;

    public GetByEmailCommand(
        IApplicationUsersRepository applicationUsersRepository,
        //IBlobService blobService,
        IMapper mapper)
    {
        _applicationUsersRepository = applicationUsersRepository;
        //_blobService = blobService;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetByEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _applicationUsersRepository.GetUser(request.Email);
        if (user == null)
        {
            return Result.Failure(["User not found"]);
        }

        var response = _mapper.Map<GetByEmailResponseDto>(user);
        //response.ProfilePicture = _blobService.GetFileAsync(user.ProfilePicture, "LjhBackendApi", user.PictureFolderName.ToString()).Result;

        return Result.Success(response);
    }
}
