using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Application.QuerySide.ViewModels;
using CalendarAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPI.Application.QuerySide.Queries.GetAllCalendarEventsFilter
{
    public class
        GetCalendarEventsFilterQueryHandler : IRequestHandler<GetCalendarEventsFilterQuery, CalendarEventViewModel[]>
    {
        private readonly CalendarContext _context;

        public GetCalendarEventsFilterQueryHandler(CalendarContext context)
        {
            _context = context;
        }

        public async Task<CalendarEventViewModel[]> Handle(
            GetCalendarEventsFilterQuery request,
            CancellationToken cancellationToken)
        {
            var calendarEvents = from ce in _context.CalendarEvents.AsNoTracking()
                    .Include(ce => ce.Members)
                select ce;
            var filters = request.CalendarEventFilter;

            if (filters.EventOrganizer != null)
                calendarEvents = calendarEvents.Where(ce => ce.Organizer == filters.EventOrganizer);

            if (filters.Id > 0)
                calendarEvents = calendarEvents.Where(ce => ce.Id == filters.Id);

            if (filters.Location != null)
                calendarEvents = calendarEvents.Where(ce => ce.Location == filters.Location);

            if (filters.Name != null)
                calendarEvents = calendarEvents.Where(ce => ce.Name == filters.Name);

            return await calendarEvents.Select(calendarEvent =>
                    new CalendarEventViewModel(
                        calendarEvent.Id,
                        calendarEvent.Name,
                        calendarEvent.Time,
                        calendarEvent.Location,
                        calendarEvent.Organizer,
                        calendarEvent.Members.Select(m => m.Name).ToArray()
                    )
                )
                .ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}