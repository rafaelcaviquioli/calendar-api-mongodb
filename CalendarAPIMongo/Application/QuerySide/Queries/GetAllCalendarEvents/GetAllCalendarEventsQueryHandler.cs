using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using CalendarAPIMongo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEvents
{
    public class GetAllCalendarEventsQueryHandler : IRequestHandler<GetAllCalendarEventsQuery, CalendarEventViewModel[]>
    {
        private readonly CalendarContext _context;

        public GetAllCalendarEventsQueryHandler(CalendarContext context)
        {
            _context = context;
        }

        public async Task<CalendarEventViewModel[]> Handle(GetAllCalendarEventsQuery request,
            CancellationToken cancellationToken)
            => await _context.CalendarEvents
                .Select(calendarEvent =>
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