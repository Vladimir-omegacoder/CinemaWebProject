using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;

namespace Server.Services;

public partial class CinemaContext : DbContext
{
    public CinemaContext()
    {
    }

    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<HallSeat> HallSeats { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<MovieSchedule> MovieSchedules { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RRURPVM;Database=Cinema;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3213E83F526BA02E");

            entity.ToTable("Booking");

            entity.HasIndex(e => new { e.TicketId, e.SeatId }, "UQ__Booking__0C9027B3F639F93B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("booking_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.SeatId).HasColumnName("seat_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Booking__custome__31B762FC");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Booking__ticket___32AB8735");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FB102BC7D");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.UserId, "UQ__Customer__B9BE370EA0D3B75B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .HasConstraintName("FK__Customer__user_i__2CF2ADDF");
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hall__3213E83F99D83A27");

            entity.ToTable("Hall");

            entity.HasIndex(e => e.Name, "UQ__Hall__72E12F1BAD9E59EA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<HallSeat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HallSeat__3213E83F551768CB");

            entity.ToTable("HallSeat");

            entity.HasIndex(e => new { e.HallId, e.RowNumber, e.SeatNumber }, "UQ__HallSeat__4F3D4F4884C981A5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");

            entity.HasOne(d => d.Hall).WithMany(p => p.HallSeats)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__HallSeat__hall_i__5AEE82B9");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movie__3213E83F710916D7");

            entity.ToTable("Movie");

            entity.HasIndex(e => e.Title, "UQ__Movie__E52A1BB3ECF040B1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeRestrictions).HasColumnName("age_restrictions");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Movie__genre_id__4AB81AF0");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieGen__3213E83FF621C2F5");

            entity.ToTable("MovieGenre");

            entity.HasIndex(e => e.Name, "UQ__MovieGen__72E12F1B24292C07").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MovieSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieSch__3213E83FF67921EC");

            entity.ToTable("MovieSchedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.ShowDate).HasColumnName("show_date");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.Hall).WithMany(p => p.MovieSchedules)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__MovieSche__hall___5535A963");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieSchedules)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__MovieSche__movie__5441852A");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3213E83FC6844412");

            entity.ToTable("Permission");

            entity.HasIndex(e => new { e.TableName, e.Operation }, "UQ__Permissi__0F39A25366339217").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Operation)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("operation");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("table_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FF680D43F");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "UQ__Role__72E12F1B1340B49D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3213E83F151C9B15");

            entity.ToTable("RolePermission");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "UQ__RolePerm__C85A5462709A35E8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK__RolePermi__permi__489AC854");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__RolePermi__role___47A6A41B");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3213E83F4EE9E0C8");

            entity.ToTable("Ticket");

            entity.HasIndex(e => e.ScheduleId, "UQ__Ticket__C46A8A6E4EAF5BFE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

            entity.HasOne(d => d.Schedule).WithOne(p => p.Ticket)
                .HasForeignKey<Ticket>(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__price__17F790F9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F4ED1A997");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616439F4F206").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_salt");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3213E83FB7179056");

            entity.ToTable("UserRole");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "UQ__UserRole__6EDEA152D2804C6B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRole__role_i__51300E55");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRole__user_i__503BEA1C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
