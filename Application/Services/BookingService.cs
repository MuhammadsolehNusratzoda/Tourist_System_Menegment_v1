public class BookingService : IBookingService
{
    private readonly IBookingRepository _repo;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context; // to look up prices

    public BookingService(IBookingRepository repo, IMapper mapper, AppDbContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiResponse<BookingDto>> CreateAsync(CreateBookingDto dto, Guid userId)
    {
        // Calculate price based on type
        decimal totalPrice = 0;
        var days = (dto.EndDate - dto.StartDate).Days;
        if (days <= 0) return ApiResponse<BookingDto>.Fail("Invalid dates.");

        if (dto.BookingType == "Hotel")
        {
            var hotel = await _context.Hotels.FindAsync(dto.ReferenceId);
            if (hotel == null) return ApiResponse<BookingDto>.Fail("Hotel not found.");
            totalPrice = hotel.PricePerNight * days;
        }
        else if (dto.BookingType == "Guide")
        {
            var guide = await _context.Guides.FindAsync(dto.ReferenceId);
            if (guide == null) return ApiResponse<BookingDto>.Fail("Guide not found.");
            totalPrice = guide.PricePerDay * days;
        }
        else if (dto.BookingType == "Transport")
        {
            var transport = await _context.Transports.FindAsync(dto.ReferenceId);
            if (transport == null) return ApiResponse<BookingDto>.Fail("Transport not found.");
            totalPrice = transport.PricePerSeat;
        }

        var booking = new Booking
        {
            UserId = userId,
            BookingType = dto.BookingType,
            ReferenceId = dto.ReferenceId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = totalPrice,
            Notes = dto.Notes,
            Status = "Pending"
        };

        await _repo.AddAsync(booking);
        return ApiResponse<BookingDto>.Ok(_mapper.Map<BookingDto>(booking), "Booking created.");
    }

    public async Task<ApiResponse<bool>> CancelAsync(Guid id, Guid userId, string role)
    {
        var booking = await _repo.GetByIdAsync(id);
        if (booking == null) return ApiResponse<bool>.Fail("Not found.");

        if (role != "Admin" && booking.UserId != userId)
            return ApiResponse<bool>.Fail("Access denied.");

        if (booking.Status == "Cancelled")
            return ApiResponse<bool>.Fail("Already cancelled.");

        booking.Status = "Cancelled";
        await _repo.UpdateAsync(booking);
        return ApiResponse<bool>.Ok(true, "Booking cancelled.");
    }

    public async Task<ApiResponse<PagedResult<BookingDto>>> GetUserBookingsAsync(
        Guid userId, int page, int pageSize)
    {
        var result = await _repo.GetByUserIdAsync(userId, page, pageSize);
        var dtos = new PagedResult<BookingDto>
        {
            Items = _mapper.Map<List<BookingDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
        return ApiResponse<PagedResult<BookingDto>>.Ok(dtos);
    }

    public async Task<ApiResponse<PagedResult<BookingDto>>> GetAllAsync(int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(page, pageSize);
        var dtos = new PagedResult<BookingDto>
        {
            Items = _mapper.Map<List<BookingDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
        return ApiResponse<PagedResult<BookingDto>>.Ok(dtos);
    }
}