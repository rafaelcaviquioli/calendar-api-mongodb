using CalendarAPIMongo.Application.QuerySide.ViewModels;
using MediatR;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEvents
{
    public class GetAllCalendarEventsQuery : IRequest<CalendarEventViewModel[]>
    {
    }
}