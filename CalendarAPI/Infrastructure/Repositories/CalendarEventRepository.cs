using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPI.Infrastructure.Repositories
{
    public class CalendarEventRepository : IEventRepository
    {
        private readonly CalendarContext _context;

        public CalendarEventRepository(CalendarContext context)
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
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CalendarEvent> List()
        {
            throw new System.NotImplementedException();
        }
    }
}