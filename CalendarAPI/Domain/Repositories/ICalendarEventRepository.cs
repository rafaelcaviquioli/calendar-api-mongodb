using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;

namespace CalendarAPI.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        Task Save(CalendarEvent calendarEvent);
        Task<CalendarEvent> FindOne(int id);
        Task Remove(CalendarEvent calendarEvent);
        IEnumerable<CalendarEvent> List();
    }
}