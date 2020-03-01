using CalendarAPIMongo.Application.QuerySide.Filters;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using MediatR;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEventsFilter
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