using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarAPIMongo.Domain.Models;
using CalendarAPIMongo.Domain.Repositories;
using MongoDB.Driver;

namespace CalendarAPIMongo.Infrastructure.Repositories
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private readonly MongoDbContext _context;

        public CalendarEventRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task Update(CalendarEvent calendarEventIn)
            => await _context.calendarEvents.ReplaceOneAsync(
                calendarEvent => calendarEvent.Id == calendarEventIn.Id,
                calendarEventIn
            );

        public async Task Insert(CalendarEvent calendarEventIn)
            => await _context.calendarEvents.InsertOneAsync(calendarEventIn);

        public async Task<CalendarEvent> FindOne(string id) =>
            await _context.calendarEvents
                .Find<CalendarEvent>(calendarEvent => calendarEvent.Id == id)
                .FirstOrDefaultAsync();

        public async Task Remove(CalendarEvent calendarEventIn) =>
            await _context.calendarEvents.DeleteOneAsync(calendarEvent => calendarEvent.Id == calendarEventIn.Id);

        public IEnumerable<CalendarEvent> List() =>
            _context.calendarEvents.AsQueryable<CalendarEvent>();
    }
}