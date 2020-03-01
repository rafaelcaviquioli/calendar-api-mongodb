using System.Threading.Tasks;
using CalendarAPIMongo.Application.CommandSide.Commands.AddNewCalendarEvent;
using CalendarAPIMongo.Application.CommandSide.Commands.EditCalendarEvent;
using CalendarAPIMongo.Application.CommandSide.Commands.RemoveCalendarEvent;
using CalendarAPIMongo.Application.Exceptions;
using CalendarAPIMongo.Application.QuerySide.Filters;
using CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEvents;
using CalendarAPIMongo.Application.QuerySide.Queries.GetAllCalendarEventsFilter;
using CalendarAPIMongo.Application.QuerySide.Queries.GetCalendarEventsSortedPerTime;
using CalendarAPIMongo.Application.QuerySide.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarAPIMongo.Web.Controllers
{
    [ApiController]
    [Route("calendar")]
    public class EventCalendarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventCalendarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddNewEvent([FromBody] AddNewCalendarEventCommand command)
        {
            var id = await _mediator.Send(command);

            return Created($"calendar/{id}", id);
        }

        [HttpDelete, Route("/{calendarEventId}")]
        public async Task<ActionResult> RemoveCalendarEvent([FromRoute] string calendarEventId)
        {
            try
            {
                var command = new RemoveCalendarEventCommand(calendarEventId);
                await _mediator.Send(command);

                return Ok();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound(calendarEventId);
            }
        }

        [HttpPut, Route("/{calendarEventId}")]
        public async Task<ActionResult> EditCalendarEvent([FromRoute] string calendarEventId,
            [FromBody] EditCalendarEventCommand command)
        {
            try
            {
                command.Id = calendarEventId;
                await _mediator.Send(command);

                return Ok();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound(calendarEventId);
            }
        }

        [HttpGet]
        public async Task<ActionResult<CalendarEventViewModel[]>> GetAllCalendarEvents()
        {
            var request = new GetAllCalendarEventsQuery();
            var calendarEvents = await _mediator.Send(request);

            return Ok(calendarEvents);
        }
        
        [HttpGet, Route("query")]
        public async Task<ActionResult<CalendarEventViewModel[]>> GetCalendarEventsFilter([FromQuery] CalendarEventFilter calendarEventFilter)
        {
            var request = new GetCalendarEventsFilterQuery(calendarEventFilter);
            var calendarEvents = await _mediator.Send(request);

            return Ok(calendarEvents);
        }
        
        [HttpGet, Route("sort")]
        public async Task<ActionResult<CalendarEventViewModel[]>> GetCalendarEventsSortedPerTime()
        {
            var request = new GetCalendarEventsSortedPerTimeQuery();
            var calendarEvents = await _mediator.Send(request);

            return Ok(calendarEvents);
        }
    }
}