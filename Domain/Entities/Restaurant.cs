public class Restaurant : Base
{
    public string Name {get; set;}=string.Empty;
    public string? Description {get; set;}
    public string Location {get; set;}=string.Empty;
    public string CoisineType {get; set;}=string.Empty; // Tajik, Italian, etc.
    public string? PriceRange {get; set;}
    public string? OpeningHours {get; set;}
    public string? ImageUrl {get; set;}
    public string PhoneNumber {get; set;}=string.Empty;
    public decimal Rating {get; set;}=0;
    public Guid OwnerId {get; set;}
    public User? Owner {get; set;}

}