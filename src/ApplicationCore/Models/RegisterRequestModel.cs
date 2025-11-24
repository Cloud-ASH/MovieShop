using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class RegisterRequestModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    public string? PhoneNumber { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
