using System.ComponentModel.DataAnnotations;

namespace LjhBackendApi.Application.Features.ApplicationUsers.Commands.Register;
public class RegistrationDto
{
#pragma warning disable 8632

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //public string ConfirmPassword { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid zip code format.")]
    public string? ZipCode { get; set; }

    //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //public string ConfirmPassword { get; set; }
    
}

