using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarAPIMongo.Domain.Models;

namespace CalendarAPIMongo.Domain.Repositories
{
    public interface ICalendarEventRepository
    {
        Task Update(CalendarEvent calendarEvent);
        Task Insert(CalendarEvent calendarEvent);
        Task<CalendarEvent> FindOne(string id);
        Task Remove(CalendarEvent calendarEvent);
        IEnumerable<CalendarEvent> List();
    }
}