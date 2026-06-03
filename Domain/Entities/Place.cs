public class Place : Base
{
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    public string Location { get; set; }=string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ImageUrl { get; set; }=string.Empty;
    public decimal EntryFee { get; set; }=0;
    public decimal Rating { get; set; }=0;
    public Guid CreatedByUserId{get; set;}
    // Navigation
    public User? CreatedBy { get; set; }
    public List<Review> Reviews { get; set; }=new List<Review>();
}