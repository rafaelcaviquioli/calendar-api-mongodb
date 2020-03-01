using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using CalendarAPIMongo.Domain.Repositories;
using MediatR;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetCalendarEventsSortedPerTime
{
    public class GetCalendarEventsSortedPerTimeQueryHandler : IRequestHandler<GetCalendarEventsSortedPerTimeQuery, CalendarEventViewModel[]>
    {
        private readonly ICalendarEventRepository _repository;

        public GetCalendarEventsSortedPerTimeQueryHandler(ICalendarEventRepository repository)
        {
            _repository = repository;
        }

        public Task<CalendarEventViewModel[]> Handle(
            GetCalendarEventsSortedPerTimeQuery request,
            CancellationToken cancellationToken
        )
        {
            var calendarEvents = _repository.List()
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
                .ToArray();

            return Task.FromResult(calendarEvents);
        }
    }
}