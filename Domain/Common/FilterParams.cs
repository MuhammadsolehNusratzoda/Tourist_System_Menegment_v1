public class FilterParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public string? SortBy { get; set; }       // e.g. "Name", "Rating", "Price"
    public bool IsDescending { get; set; } = false;
}

public class GuideFilterParams : FilterParams
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Language { get; set; }       // "English", "Tajik"
    public int? MinExperience { get; set; }
    public decimal? MinRating { get; set; }
}

public class TransportFilterParams : FilterParams
{
    public string? Type { get; set; }           // Bus, Taxi, Train, Car
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinAvailableSeats { get; set; }
    public DateTime? DepartureFrom { get; set; }
    public DateTime? DepartureTo { get; set; }
}

public class RestaurantFilterParams : FilterParams
{
    public string? Location { get; set; }
    public string? CuisineType { get; set; }    // Tajik, Italian, etc.
    public string? PriceRange { get; set; }     // $, $$, $$$
    public decimal? MinRating { get; set; }
}