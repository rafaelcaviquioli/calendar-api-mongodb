using CalendarAPI.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPI.Infrastructure
{
    public class CalendarContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Members)
                .WithOne(m => m.Event)
                .HasForeignKey(m => m.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}