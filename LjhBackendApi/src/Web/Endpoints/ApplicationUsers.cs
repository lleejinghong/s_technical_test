using LjhBackendApi.Application.Features.ApplicationUsers.Commands.DeleteUser;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.Login;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.RefreshToken;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.Register;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.Registration;
using LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetByEmail;
using LjhBackendApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.UpdateUser;

namespace LjhBackendApi.Web.Endpoints;

public class ApplicationUsers : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(LoginUser, "LoginUser")
            .MapPost(RefreshToken, "RefreshToken")
            .MapPost(RegisterUser, "RegisterUser")
            .MapDelete(DeleteUser, "DeleteUser/{email}")
            .MapPut(UpdateUser, "UpdateUser")
            .MapGet(GetByEmail, "GetByEmail/{email}");

    }

    [AllowAnonymous]
    public async Task<IResult> LoginUser([FromBody] LoginDto dto, ISender sender)
    {
        var loginRequest = new LoginRequest
        {
            Username = dto.Username,
            Password = dto.Password,
            RememberMe = dto.RememberMe
        };
        var response = await sender.Send(loginRequest);
        return response.Succeeded ? Results.Ok(response.Data) : Results.BadRequest(response.Errors);
    }

    [AllowAnonymous]
    public async Task<IResult> RefreshToken([FromBody] Tokens token, ISender sender)
    {
        var refreshTokenRequest = new RefreshTokenRequest
        {
            OldToken = token
        };
        var response = await sender.Send(refreshTokenRequest);
        return response.Succeeded ? Results.Ok(response.Data) : Results.Unauthorized();
    }

    [AllowAnonymous]
    public async Task<IResult> RegisterUser([FromBody] RegistrationDto registrationDto, ISender sender)
    {
        var request = new RegistrationRequest
        {
            RegistrationDto = registrationDto
        };

        var response = await sender.Send(request);
        return response.Succeeded ? Results.Ok(response.Data) : Results.BadRequest(response);
    }


    public async Task<IResult> DeleteUser(string email, ISender sender)
    {
        var request = new DeleteUserRequest
        {
            Email = email
        };
        var response = await sender.Send(request);
        return response.Succeeded ? Results.Ok(response.Data) : Results.BadRequest(response);
    }

    public async Task<IResult> GetByEmail(string email, ISender sender)
    {
        var request = new GetByEmailRequest
        {
            Email = email
        };
        var response = await sender.Send(request);
        return response.Succeeded ? Results.Ok(response.Data) : Results.BadRequest(response.Errors);
    }

    public async Task<IResult> UpdateUser(UpdateUserDto dto, ISender sender)
    {
        var request = new UpdateUserRequest
        {
            UpdateUserDto = dto
        };

        var response = await sender.Send(request);
        return response.Succeeded ? Results.Ok(response.Data) : Results.BadRequest(response.Errors);
    }
}
