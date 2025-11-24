using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Genre
{
    public int Id { get; set; }

    [Required]
    [MaxLength(24)]
    public string Name { get; set; }

    // Navigation property
    public ICollection<MovieGenre>? MovieGenres { get; set; }
}
