
namespace LjhBackendApi.Domain.Entities;
public class ApplicationUser : BaseAuditableEntity
{
    public Guid Id { get; set; } //usually use long
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string ZipCode { get; set; }
    public string? ProfilePicture { get; set; }

}
