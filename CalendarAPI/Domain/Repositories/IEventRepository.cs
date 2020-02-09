using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;

namespace CalendarAPI.Domain.Repositories
{
    public interface IEventRepository
    {
        Task Save(CalendarEvent calendarEvent);
        Task<CalendarEvent> FindOne(int id);
        Task Remove(int id);
        IEnumerable<CalendarEvent> List();
    }
}