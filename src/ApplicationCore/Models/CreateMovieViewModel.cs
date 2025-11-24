using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class CreateMovieViewModel
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Overview { get; set; }

    public string? TagLine { get; set; }

    public decimal Budget { get; set; }

    public decimal Revenue { get; set; }

    public string? ImdbURL { get; set; }

    public string? TmdbUrl { get; set; }

    public string? PosterUrl { get; set; }

    public string? BackdropUrl { get; set; }

    public string? OriginalLanguage { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? RunTime { get; set; }

    public decimal? Price { get; set; }

    // Selected genre IDs
    public List<int> SelectedGenreIds { get; set; } = new List<int>();
}
