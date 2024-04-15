using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class UserForAuthenticationDto
{
    /// <example>JBloggs</example>
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }
    /// <example>P*********</example>
    [Required(ErrorMessage = "Password name is required")]
    public string Password { get; set; }
}