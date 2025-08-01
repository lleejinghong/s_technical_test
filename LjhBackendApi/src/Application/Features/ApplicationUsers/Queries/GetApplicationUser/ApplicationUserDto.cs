using System.ComponentModel.DataAnnotations;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands;

namespace LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetApplicationUser;
public class ApplicationUserDto
{
#pragma warning disable 8632

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ZipCode { get; set; }
    public string? ProfilePicture { get; set; }

}
