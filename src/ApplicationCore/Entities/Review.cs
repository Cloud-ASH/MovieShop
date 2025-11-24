using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class Review
{
    public int MovieId { get; set; }
    public int UserId { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation properties
    public Movie Movie { get; set; }
    public User User { get; set; }
}
