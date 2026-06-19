using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets — each table
    public DbSet<User> Users => Set<User>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Transport> Transports => Set<Transport>();
    public DbSet<Guide> Guides => Set<Guide>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.FullName).HasMaxLength(200);
            e.Property(u => u.Email).HasMaxLength(200);
            e.HasIndex(u => u.Email).IsUnique();
        });

        // Place
        modelBuilder.Entity<Place>(e =>
        {
            e.HasKey(p => p.Id);
            e.HasOne(p => p.CreatedBy).WithMany().HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Hotel
        modelBuilder.Entity<Hotel>(e =>
        {
            e.HasKey(h => h.Id);
            e.HasOne(h => h.Owner).WithMany().HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Guide
        modelBuilder.Entity<Guide>(e =>
        {
            e.HasKey(g => g.Id);
            e.HasOne(g => g.User).WithMany().HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Restaurant
        modelBuilder.Entity<Restaurant>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasOne(r => r.Owner).WithMany().HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Transport
        modelBuilder.Entity<Transport>(e =>
        {
            e.HasKey(t => t.Id);
            e.HasOne(t => t.Owner).WithMany().HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Booking
        modelBuilder.Entity<Booking>(e =>
        {
            e.HasKey(b => b.Id);
            e.HasOne(b => b.User).WithMany(u => u.Bookings).HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Review
        modelBuilder.Entity<Review>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}