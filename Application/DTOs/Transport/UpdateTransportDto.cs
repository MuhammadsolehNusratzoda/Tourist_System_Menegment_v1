public class UpdateTransportDto
{   
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public DateTime? DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public decimal? PricePerSeat { get; set; }
    public int? AvailableSeats { get; set; }
}