using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using CalendarAPIMongo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetCalendarEventsSortedPerTime
{
    public class GetCalendarEventsSortedPerTimeQueryHandler : IRequestHandler<GetCalendarEventsSortedPerTimeQuery, CalendarEventViewModel[]>
    {
        private readonly CalendarContext _context;

        public GetCalendarEventsSortedPerTimeQueryHandler(CalendarContext context)
        {
            _context = context;
        }

        public async Task<CalendarEventViewModel[]> Handle(GetCalendarEventsSortedPerTimeQuery request,
            CancellationToken cancellationToken)
            => await _context.CalendarEvents
                .OrderByDescending(ce => ce.Time)
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