public class CreateTransportDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Bus, Taxi, Train, Car
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal PricePerSeat { get; set; }
    public int AvailableSeats { get; set; }
    public Guid OwnerId { get; set; }
}