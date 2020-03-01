using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using CalendarAPIMongo.Domain.Repositories;
using MediatR;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEvents
{
    public class GetAllCalendarEventsQueryHandler : IRequestHandler<GetAllCalendarEventsQuery, CalendarEventViewModel[]>
    {
        private readonly ICalendarEventRepository _repository;

        public GetAllCalendarEventsQueryHandler(ICalendarEventRepository repository)
        {
            _repository = repository;
        }

        public Task<CalendarEventViewModel[]> Handle(
            GetAllCalendarEventsQuery request,
            CancellationToken cancellationToken)
        {
            var calendarEvents = _repository.List()
                .Select(calendarEvent =>
                    new CalendarEventViewModel(
                        calendarEvent.Id,
                        calendarEvent.Name,
                        calendarEvent.Time,
                        calendarEvent.Location,
                        calendarEvent.Organizer,
                        calendarEvent.Members.Select(m => m.Name).ToArray()
                    )
                ).ToArray();

            return Task.FromResult(calendarEvents);
        }
    }
}