public class Hotel : Base
{
    public string Name {get; set;}=string.Empty;
    public string Description {get; set;}=string.Empty;
    public string Location {get; set;}=string.Empty;
    public decimal PricePerNight {get; set;}
    public int Stars {get; set;} // 1 to 5
    public string ImageUrl {get; set;}=string.Empty;
    public decimal Rating {get; set;}=0;
    public Guid OwnerId {get; set;}
    // Navigation
    public User? Owner {get; set;}
    public List<Booking> Bookings { get; set; } = new List<Booking>();
    public List<Review> Reviews { get; set; } = new List<Review>(); 
    
}