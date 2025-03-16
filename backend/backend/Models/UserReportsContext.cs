using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class userreportsContext : DbContext
{
    public userreportsContext()
    {
    }

    public userreportsContext(DbContextOptions<userreportsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Report> reports { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<verification> verifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseMySql(config.GetConnectionString("UserReports"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.HasIndex(e => e.UserID, "Reports UserID - Users ID");

            entity.Property(e => e.Customer).HasColumnType("tinytext");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Occurrence).HasColumnType("datetime");
            entity.Property(e => e.Title).HasColumnType("tinytext");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("Reports UserID - Users ID");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.Property(e => e.Name).HasColumnType("tinytext");
        });

        modelBuilder.Entity<verification>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("verification");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.Code).HasMaxLength(6);
            entity.Property(e => e.Created_at)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.Email).HasMaxLength(320);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
