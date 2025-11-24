using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Role
{
    public int Id { get; set; }

    [MaxLength(20)]
    public string? Name { get; set; }

    // Navigation property
    public ICollection<UserRole>? UserRoles { get; set; }
}
