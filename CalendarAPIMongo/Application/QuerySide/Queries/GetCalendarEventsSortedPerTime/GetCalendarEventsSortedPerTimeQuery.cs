using CalendarAPIMongo.Application.QuerySide.ViewModels;
using MediatR;

namespace CalendarAPIMongo.Application.QuerySide.Queries.GetCalendarEventsSortedPerTime
{
    public class GetCalendarEventsSortedPerTimeQuery : IRequest<CalendarEventViewModel[]>
    {
    }
}