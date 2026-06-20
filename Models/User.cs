using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "The name must be at least 40 character long")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "The email must be a valid @gmail.com address.")]
    public string Email { get; set; } = string.Empty;
}