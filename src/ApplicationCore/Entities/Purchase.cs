using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class Purchase
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public Guid PurchaseNumber { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal TotalPrice { get; set; }

    public DateTime PurchaseDateTime { get; set; }

    public int MovieId { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Movie Movie { get; set; }
}
