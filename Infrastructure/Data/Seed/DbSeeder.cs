using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext ctx)
    {
        await ctx.Database.MigrateAsync();

        // Only seed if empty
        if (await ctx.Users.AnyAsync()) return;

        // ─── Users ───────────────────────────────────────────────────────────────
        var adminId = Guid.NewGuid();
        var tourist1Id = Guid.NewGuid();
        var guide1UserId = Guid.NewGuid();
        var guide2UserId = Guid.NewGuid();
        var ownerUserId = Guid.NewGuid();

        var users = new List<User>
        {
            new User { Id = adminId, FullName = "Muhammadsoleh Nusratzoda", Email = "admin@touristan.tj",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), Role = "Admin", IsActive = true,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = tourist1Id, FullName = "John Traveler", Email = "john@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Tourist@123"), Role = "Tourist", IsActive = true,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = guide1UserId, FullName = "Ahmad Rahimov", Email = "ahmad@touristan.tj",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Guide@123"), Role = "Guide", IsActive = true,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = guide2UserId, FullName = "Zulfiya Nazarova", Email = "zulfiya@touristan.tj",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Guide@123"), Role = "Guide", IsActive = true,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = ownerUserId, FullName = "Hotel Manager", Email = "owner@touristan.tj",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Owner@123"), Role = "Tourist", IsActive = true,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };
        await ctx.Users.AddRangeAsync(users);

        // ─── Hotels ───────────────────────────────────────────────────────────────
        var hotels = new List<Hotel>
        {
            new Hotel { Id = Guid.NewGuid(), Name = "Dushanbe Grand Hotel", Location = "Dushanbe",
                Description = "A luxurious 5-star hotel in the heart of Dushanbe, offering world-class amenities and stunning views of the Hissar Mountains.",
                PricePerNight = 120, Stars = 5, Rating = 4.8m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Hotel { Id = Guid.NewGuid(), Name = "Khujand Palace", Location = "Khujand",
                Description = "Elegant hotel by the Syr Darya river, the perfect base for exploring northern Tajikistan.",
                PricePerNight = 75, Stars = 4, Rating = 4.5m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Hotel { Id = Guid.NewGuid(), Name = "Pamir Lodge", Location = "Khorog",
                Description = "Rustic yet comfortable lodge nestled in the majestic Pamir mountains, perfect for adventurers.",
                PricePerNight = 45, Stars = 3, Rating = 4.6m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Hotel { Id = Guid.NewGuid(), Name = "Istaravshan Heritage Inn", Location = "Istaravshan",
                Description = "Historic inn located in the ancient silk road city, with traditional Tajik architecture.",
                PricePerNight = 55, Stars = 3, Rating = 4.3m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Hotel { Id = Guid.NewGuid(), Name = "Iskanderkul Resort", Location = "Sughd",
                Description = "Breathtaking mountain resort by the legendary Iskanderkul lake, surrounded by pristine nature.",
                PricePerNight = 90, Stars = 4, Rating = 4.7m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1571896349842-33c89424de2d?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Hotel { Id = Guid.NewGuid(), Name = "Wakhan Valley Guesthouse", Location = "Wakhan",
                Description = "Simple but charming guesthouse in the remote Wakhan corridor with incredible views.",
                PricePerNight = 30, Stars = 2, Rating = 4.4m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };
        await ctx.Hotels.AddRangeAsync(hotels);

        // ─── Places ───────────────────────────────────────────────────────────────
        var places = new List<Place>
        {
            new Place { Id = Guid.NewGuid(), Name = "Iskanderkul Lake", Location = "Sughd Region",
                Description = "A stunning turquoise mountain lake named after Alexander the Great, nestled at 2,200m altitude.",
                Latitude = 39.0844, Longitude = 68.3753, EntryFee = 5, Rating = 4.9m, CreatedByUserId = adminId,
                ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Place { Id = Guid.NewGuid(), Name = "Hissar Fortress", Location = "Hissar",
                Description = "Ancient fortress with over 3,000 years of history, once a strategic silk road gateway.",
                Latitude = 38.5314, Longitude = 68.5455, EntryFee = 3, Rating = 4.6m, CreatedByUserId = adminId,
                ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Place { Id = Guid.NewGuid(), Name = "Pamir Highway", Location = "GBAO Region",
                Description = "One of the world's highest roads (M41), offering breathtaking views across the Roof of the World.",
                Latitude = 37.5000, Longitude = 73.0000, EntryFee = 0, Rating = 4.8m, CreatedByUserId = adminId,
                ImageUrl = "https://images.unsplash.com/photo-1519608487953-e999c86e7455?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Place { Id = Guid.NewGuid(), Name = "Seven Lakes", Location = "Sughd",
                Description = "A magical chain of 7 turquoise mountain lakes in the Fan Mountains, perfect for trekking.",
                Latitude = 39.2200, Longitude = 68.2800, EntryFee = 2, Rating = 4.9m, CreatedByUserId = adminId,
                ImageUrl = "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Place { Id = Guid.NewGuid(), Name = "National Museum of Tajikistan", Location = "Dushanbe",
                Description = "Learn about Tajikistan's rich history and culture, from ancient Aryan civilization to modern times.",
                Latitude = 38.5598, Longitude = 68.7738, EntryFee = 4, Rating = 4.3m, CreatedByUserId = adminId,
                ImageUrl = "https://images.unsplash.com/photo-1554907984-15263bfd63bd?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Place { Id = Guid.NewGuid(), Name = "Wakhan Corridor", Location = "GBAO",
                Description = "Remote valley bordering Afghanistan and China, with ancient petroglyphs and incredible mountain views.",
                Latitude = 37.2000, Longitude = 73.5000, EntryFee = 0, Rating = 4.7m, CreatedByUserId = adminId,
                ImageUrl = "https://images.unsplash.com/photo-1569949381669-ecf31ae8e613?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };
        await ctx.Places.AddRangeAsync(places);

        // ─── Guides ───────────────────────────────────────────────────────────────
        var guides = new List<Guide>
        {
            new Guide { Id = Guid.NewGuid(), UserId = guide1UserId, Name = "Ahmad Rahimov",
                Bio = "Professional mountain guide with 10 years of experience in the Pamirs. Speaks English, Russian and Tajik.",
                Languages = "Tajik, English, Russian", PricePerDay = 60, ExperienceYears = 10, Rating = 4.9m,
                ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=400",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Guide { Id = Guid.NewGuid(), UserId = guide2UserId, Name = "Zulfiya Nazarova",
                Bio = "Cultural guide specializing in Dushanbe's historical sites and Tajik cuisine tours.",
                Languages = "Tajik, English, Persian", PricePerDay = 45, ExperienceYears = 6, Rating = 4.8m,
                ImageUrl = "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=400",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Guide { Id = Guid.NewGuid(), UserId = adminId, Name = "Rustam Karimov",
                Bio = "Adventure trekking guide certified for high-altitude expeditions in the Fan Mountains.",
                Languages = "Tajik, Russian, German", PricePerDay = 70, ExperienceYears = 12, Rating = 4.7m,
                ImageUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=400",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Guide { Id = Guid.NewGuid(), UserId = ownerUserId, Name = "Malika Tursunova",
                Bio = "Silk Road history expert offering guided tours through ancient caravanserai and bazaars.",
                Languages = "Tajik, English, French", PricePerDay = 50, ExperienceYears = 8, Rating = 4.6m,
                ImageUrl = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=400",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Guide { Id = Guid.NewGuid(), UserId = tourist1Id, Name = "Behruz Saidov",
                Bio = "Wildlife and bird-watching specialist with deep knowledge of Tajikistan's ecosystems.",
                Languages = "Tajik, English", PricePerDay = 55, ExperienceYears = 9, Rating = 4.5m,
                ImageUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=400",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };
        await ctx.Guides.AddRangeAsync(guides);

        // ─── Restaurants ────────────────────────────────────────────────────────
        var restaurants = new List<Restaurant>
        {
            new Restaurant { Id = Guid.NewGuid(), Name = "Chayhona Rohat", Location = "Dushanbe",
                Description = "Traditional Tajik teahouse with an outdoor garden serving authentic Tajik cuisine.",
                CoisineType = "Tajik", PriceRange = "$$", OpeningHours = "08:00-23:00",
                PhoneNumber = "+992 37 221-1234", Rating = 4.7m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Restaurant { Id = Guid.NewGuid(), Name = "Pamir Restaurant", Location = "Dushanbe",
                Description = "Rooftop dining with panoramic views of the city. Specializes in Central Asian and Pamiri dishes.",
                CoisineType = "Central Asian", PriceRange = "$$$", OpeningHours = "12:00-00:00",
                PhoneNumber = "+992 37 227-5678", Rating = 4.5m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1555396273-367ea4eb4db5?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Restaurant { Id = Guid.NewGuid(), Name = "Silk Road Cafe", Location = "Khujand",
                Description = "Cozy cafe on the ancient Silk Road offering fusion cuisine blending Tajik and Mediterranean flavors.",
                CoisineType = "Fusion", PriceRange = "$$", OpeningHours = "09:00-22:00",
                PhoneNumber = "+992 34 226-9012", Rating = 4.4m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Restaurant { Id = Guid.NewGuid(), Name = "Hissar Grill House", Location = "Hissar",
                Description = "Authentic barbecue restaurant near the Hissar fortress, serving fresh locally-sourced meats.",
                CoisineType = "BBQ", PriceRange = "$$", OpeningHours = "11:00-22:00",
                PhoneNumber = "+992 37 235-3456", Rating = 4.6m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Restaurant { Id = Guid.NewGuid(), Name = "Yak Restaurant", Location = "Khorog",
                Description = "High-altitude dining experience in the Pamirs with warming traditional Pamiri dishes.",
                CoisineType = "Pamiri", PriceRange = "$", OpeningHours = "07:00-21:00",
                PhoneNumber = "+992 35 222-7890", Rating = 4.8m, OwnerId = ownerUserId,
                ImageUrl = "https://images.unsplash.com/photo-1466978913421-dad2ebd01d17?w=800",
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };
        await ctx.Restaurants.AddRangeAsync(restaurants);

        // ─── Transports ─────────────────────────────────────────────────────────
        var today = DateTime.Today;
        var transports = new List<Transport>
        {
            new Transport { Id = Guid.NewGuid(), Name = "Express Bus DU→KH", Type = "Bus",
                Origin = "Dushanbe", Destination = "Khujand",
                DepartureTime = today.AddHours(7), ArrivalTime = today.AddHours(14),
                PricePerSeat = 12, AvailableSeats = 28, OwnerId = ownerUserId,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Transport { Id = Guid.NewGuid(), Name = "Pamir Jeep Service", Type = "Car",
                Origin = "Dushanbe", Destination = "Khorog",
                DepartureTime = today.AddHours(6), ArrivalTime = today.AddHours(18),
                PricePerSeat = 35, AvailableSeats = 4, OwnerId = ownerUserId,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Transport { Id = Guid.NewGuid(), Name = "City Taxi DU→HI", Type = "Taxi",
                Origin = "Dushanbe", Destination = "Hissar",
                DepartureTime = today.AddHours(9), ArrivalTime = today.AddHours(10),
                PricePerSeat = 5, AvailableSeats = 3, OwnerId = ownerUserId,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Transport { Id = Guid.NewGuid(), Name = "Northern Train KH→IS", Type = "Train",
                Origin = "Khujand", Destination = "Istaravshan",
                DepartureTime = today.AddHours(8), ArrivalTime = today.AddHours(11),
                PricePerSeat = 8, AvailableSeats = 60, OwnerId = ownerUserId,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Transport { Id = Guid.NewGuid(), Name = "Fan Mountains Shuttle", Type = "Car",
                Origin = "Dushanbe", Destination = "Seven Lakes",
                DepartureTime = today.AddHours(6), ArrivalTime = today.AddHours(11).AddMinutes(30),
                PricePerSeat = 20, AvailableSeats = 8, OwnerId = ownerUserId,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Transport { Id = Guid.NewGuid(), Name = "Wakhan Corridor 4x4", Type = "Car",
                Origin = "Khorog", Destination = "Wakhan Valley",
                DepartureTime = today.AddHours(7), ArrivalTime = today.AddHours(13),
                PricePerSeat = 45, AvailableSeats = 4, OwnerId = ownerUserId,
                CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };
        await ctx.Transports.AddRangeAsync(transports);

        await ctx.SaveChangesAsync();
    }
}
