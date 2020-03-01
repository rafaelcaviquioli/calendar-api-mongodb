using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using CalendarAPIMongo.Domain.Repositories;
using MediatR;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEventsFilter
{
    public class GetCalendarEventsFilterQueryHandler : IRequestHandler<GetCalendarEventsFilterQuery, CalendarEventViewModel[]>
    {
        private readonly ICalendarEventRepository _repository;

        public GetCalendarEventsFilterQueryHandler(ICalendarEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<CalendarEventViewModel[]> Handle(
            GetCalendarEventsFilterQuery request,
            CancellationToken cancellationToken)
        {
            var calendarEvents = _repository.List();
            var filters = request.CalendarEventFilter;

            if (filters.EventOrganizer != null)
                calendarEvents = calendarEvents.Where(ce => ce.Organizer == filters.EventOrganizer);

            if (!String.IsNullOrEmpty(filters.Id))
                calendarEvents = calendarEvents.Where(ce => ce.Id == filters.Id);

            if (filters.Location != null)
                calendarEvents = calendarEvents.Where(ce => ce.Location == filters.Location);

            if (filters.Name != null)
                calendarEvents = calendarEvents.Where(ce => ce.Name == filters.Name);

            var calendarEventsFiltered = calendarEvents.Select(calendarEvent =>
                new CalendarEventViewModel(
                    calendarEvent.Id,
                    calendarEvent.Name,
                    calendarEvent.Time,
                    calendarEvent.Location,
                    calendarEvent.Organizer,
                    calendarEvent.Members.Select(m => m.Name).ToArray()
                )
            ).ToArray();

            return await Task.FromResult(calendarEventsFiltered);
        }
    }
}