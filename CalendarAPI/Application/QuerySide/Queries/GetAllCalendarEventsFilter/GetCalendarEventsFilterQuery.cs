using CalendarAPI.Application.QuerySide.Filters;
using CalendarAPI.Application.QuerySide.ViewModels;
using MediatR;

namespace CalendarAPI.Application.QuerySide.Queries.GetAllCalendarEventsFilter
{
    public class GetCalendarEventsFilterQuery : IRequest<CalendarEventViewModel[]>
    {
        public CalendarEventFilter CalendarEventFilter { get; }

        public GetCalendarEventsFilterQuery(CalendarEventFilter calendarEventFilter)
        {
            CalendarEventFilter = calendarEventFilter;
        }
    }
}