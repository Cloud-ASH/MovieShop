namespace ApplicationCore.Models;

public class UserPurchaseViewModel
{
    public int PurchaseId { get; set; }
    public int MovieId { get; set; }
    public string? MovieTitle { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public string? PurchaseNumber { get; set; }
}
