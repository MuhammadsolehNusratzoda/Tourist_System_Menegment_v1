using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Hotel
        CreateMap<Hotel, HotelDto>();
        CreateMap<CreateHotelDto, Hotel>()
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Rating, o => o.MapFrom(_ => 0m));
        CreateMap<UpdateHotelDto, Hotel>()
            .ForAllMembers(o => o.Condition((src, dst, val) => val != null));

        // Place
        CreateMap<Place, PlaceDto>();
        CreateMap<CreatePlaceDto, Place>()
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Rating, o => o.MapFrom(_ => 0m));
        CreateMap<UpdatePlaceDto, Place>()
            .ForAllMembers(o => o.Condition((src, dst, val) => val != null));

        // Guide
        CreateMap<Guide, GuideDto>();
        CreateMap<CreateGuideDto, Guide>()
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Rating, o => o.MapFrom(_ => 0m));
        CreateMap<UpdateGuideDto, Guide>()
            .ForAllMembers(o => o.Condition((src, dst, val) => val != null));

        // Restaurant — Note: Entity has CoisineType (typo), DTO has CuisineType
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.CuisineType, o => o.MapFrom(s => s.CoisineType));
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(d => d.CoisineType, o => o.MapFrom(s => s.CuisineType))
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Rating, o => o.MapFrom(_ => 0m));
        CreateMap<UpdateRestaurantDto, Restaurant>()
            .ForAllMembers(o => o.Condition((src, dst, val) => val != null));

        // Transport
        CreateMap<Transport, TransportDto>();
        CreateMap<CreateTransportDto, Transport>()
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateTransportDto, Transport>()
            .ForAllMembers(o => o.Condition((src, dst, val) => val != null));

        // Booking
        CreateMap<Booking, BookingDto>();
        CreateMap<CreateBookingDto, Booking>()
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Status, o => o.MapFrom(_ => "Pending"))
            .ForMember(d => d.TotalPrice, o => o.MapFrom(_ => 0m));

        // Review
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>()
            .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow));
    }
}
