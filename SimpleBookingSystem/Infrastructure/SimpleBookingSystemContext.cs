using Microsoft.EntityFrameworkCore;
using SimpleBookingSystemService.Infrastructure.Domain;

namespace SimpleBookingSystemService.Infrastructure
{
    public class SimpleBookingSystemContext : DbContext
    {
        public SimpleBookingSystemContext()
        {
        }

        public SimpleBookingSystemContext(DbContextOptions<SimpleBookingSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Quantity).IsRequired();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FromDate).IsRequired();

                entity.Property(e => e.ToDate).IsRequired();

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientNoAction)
                    .HasConstraintName("FK_Booking_Resource");
            });
        }
    }
}
