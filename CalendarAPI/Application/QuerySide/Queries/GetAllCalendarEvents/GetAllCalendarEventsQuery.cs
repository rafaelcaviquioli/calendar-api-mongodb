using CalendarAPI.Application.QuerySide.ViewModels;
using MediatR;

namespace CalendarAPI.Application.QuerySide.Queries.GetAllCalendarEvents
{
    public class GetAllCalendarEventsQuery : IRequest<CalendarEventViewModel[]>
    {
    }
}