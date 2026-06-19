namespace BlazorClient.Models;

public class TransportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal PricePerSeat { get; set; }
    public int AvailableSeats { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }

    public TimeSpan Duration => ArrivalTime - DepartureTime;
    public string DurationDisplay => $"{(int)Duration.TotalHours}h {Duration.Minutes}m";

    public string TypeIcon => Type switch
    {
        "Bus" => "bi-bus-front",
        "Train" => "bi-train-front",
        "Taxi" => "bi-taxi-front",
        "Car" => "bi-car-front",
        _ => "bi-truck"
    };
}

public class CreateTransportDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Bus";
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal PricePerSeat { get; set; }
    public int AvailableSeats { get; set; }
}
