namespace ApplicationCore.Entities;

public class MovieGenre
{
    public int MovieId { get; set; }
    public int GenreId { get; set; }

    // Navigation properties
    public Movie Movie { get; set; }
    public Genre Genre { get; set; }
}
