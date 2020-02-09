using CalendarAPI.Application.QuerySide.ViewModels;
using MediatR;

namespace CalendarAPI.Application.QuerySide.Queries.GetCalendarEventsSortedPerTime
{
    public class GetCalendarEventsSortedPerTimeQuery : IRequest<CalendarEventViewModel[]>
    {
    }
}