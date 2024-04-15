using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class UserForRegistrationDto
{
    /// <example>Joe</example>
    public string FirstName { get; set; }
    /// <example>Bloggs</example>
    public string LastName { get; set; }
    /// <example>JBloggs</example>
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }
    /// <example>P*******</example>

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    /// <example>jbloggs@email.com</example>
    public string Email { get; set; }
    /// <example>0123456789</example>
    public string PhoneNumber { get; set; }
    /// <example>Administrator, User</example>
    public ICollection<string> Roles { get; set; }
}