using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPI.Infrastructure.Repositories
{
    public class CalendarCalendarEventRepository : ICalendarEventRepository
    {
        private readonly CalendarContext _context;

        public CalendarCalendarEventRepository(CalendarContext context)
        {
            _context = context;
        }

        public async Task Save(CalendarEvent calendarEvent)
        {
            var entity = await _context.CalendarEvents
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == calendarEvent.Id);

            var entityAlreadyExists = entity != null;
            if (entityAlreadyExists)
            {
                _context.Update(calendarEvent);
            }
            else
            {
                _context.Add(calendarEvent);
            }

            await _context.SaveChangesAsync();
        }

        public Task<CalendarEvent> FindOne(int id)
            => _context.CalendarEvents
                .Include(ce => ce.Members)
                .FirstOrDefaultAsync(ce => ce.Id == id);

        public Task Remove(CalendarEvent calendarEvent)
        {
            _context.CalendarEvents.Remove(calendarEvent);
            return _context.SaveChangesAsync();
        }

        public IEnumerable<CalendarEvent> List()
        {
            throw new System.NotImplementedException();
        }
    }
}