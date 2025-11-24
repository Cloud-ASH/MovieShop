using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    [MaxLength(2084)]
    public string? Name { get; set; }

    [MaxLength(2084)]
    public string? TrailerUrl { get; set; }

    // Navigation property
    public Movie Movie { get; set; }
}
