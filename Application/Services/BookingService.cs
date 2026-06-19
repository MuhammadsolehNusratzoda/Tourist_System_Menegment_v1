using AutoMapper;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repo;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public BookingService(IBookingRepository repo, IMapper mapper, AppDbContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiResponse<BookingDto>> CreateAsync(CreateBookingDto dto, Guid userId)
    {
        decimal totalPrice = 0;
        var days = (dto.EndDate - dto.StartDate).Days;
        if (days <= 0) return ApiResponse<BookingDto>.Fail("End date must be after start date.");

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
            Id = Guid.NewGuid(),
            UserId = userId,
            BookingType = dto.BookingType,
            ReferenceId = dto.ReferenceId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = totalPrice,
            Notes = dto.Notes,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(booking);
        return ApiResponse<BookingDto>.Ok(_mapper.Map<BookingDto>(booking), "Booking created.");
    }

    public async Task<ApiResponse<bool>> CancelAsync(Guid id, Guid userId, string role)
    {
        var booking = await _repo.GetByIdAsync(id);
        if (booking == null) return ApiResponse<bool>.Fail("Booking not found.");

        if (role != "Admin" && booking.UserId != userId)
            return ApiResponse<bool>.Fail("Access denied.");

        if (booking.Status == "Cancelled")
            return ApiResponse<bool>.Fail("Already cancelled.");

        booking.Status = "Cancelled";
        booking.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(booking);
        return ApiResponse<bool>.Ok(true, "Booking cancelled.");
    }

    public async Task<ApiResponse<PagedResult<BookingDto>>> GetByUserIdAsync(Guid userId, int page, int pageSize)
    {
        var result = await _repo.GetByUserIdAsync(userId, page, pageSize);
        return ApiResponse<PagedResult<BookingDto>>.Ok(new PagedResult<BookingDto>
        {
            Items = _mapper.Map<List<BookingDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    // Keep backward compat alias
    public Task<ApiResponse<PagedResult<BookingDto>>> GetUserBookingsAsync(Guid userId, int page, int pageSize)
        => GetByUserIdAsync(userId, page, pageSize);

    public async Task<ApiResponse<PagedResult<BookingDto>>> GetAllAsync(int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(page, pageSize);
        return ApiResponse<PagedResult<BookingDto>>.Ok(new PagedResult<BookingDto>
        {
            Items = _mapper.Map<List<BookingDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<BookingDto>> GetByIdAsync(Guid id)
    {
        var booking = await _repo.GetByIdAsync(id);
        if (booking == null) return ApiResponse<BookingDto>.Fail("Booking not found.");
        return ApiResponse<BookingDto>.Ok(_mapper.Map<BookingDto>(booking));
    }

    public async Task<ApiResponse<bool>> UpdateStatusAsync(Guid id, string status)
    {
        var booking = await _repo.GetByIdAsync(id);
        if (booking == null) return ApiResponse<bool>.Fail("Booking not found.");
        booking.Status = status;
        booking.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(booking);
        return ApiResponse<bool>.Ok(true, $"Status updated to {status}.");
    }
}