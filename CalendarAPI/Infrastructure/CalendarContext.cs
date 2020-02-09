using CalendarAPI.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPI.Infrastructure
{
    public class CalendarContext : DbContext
    {
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Member> Members { get; set; }

        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalendarEvent>()
                .HasMany(e => e.Members)
                .WithOne()
                .HasForeignKey(m => m.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}