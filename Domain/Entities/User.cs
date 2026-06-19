public class User : Base
{
    public string FullName { get; set; }=string.Empty;
    public string Email{get; set;}=string.Empty;
    public string PasswordHash { get; set; }=string.Empty;
    public string PhoneNumber { get; set; }=string.Empty;
    public string Role { get; set; }="Tourist"; // Admin, Tourist, Guide, HotelOwner
    public  bool IsActive { get; set; }=true;
    // Navigation Properties
    public List<Booking> Bookings { get; set; }=new List<Booking>();
    public List<Review> Reviews { get; set; }=new List<Review>();
    public Guid? Guide { get; set; } // If Role=Guide бошад

}