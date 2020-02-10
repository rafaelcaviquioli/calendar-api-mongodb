using System.Threading.Tasks;
using CalendarAPI.Application.CommandSide.Commands.AddNewCalendarEvent;
using CalendarAPI.Application.CommandSide.Commands.EditCalendarEvent;
using CalendarAPI.Application.CommandSide.Commands.RemoveCalendarEvent;
using CalendarAPI.Application.Exceptions;
using CalendarAPI.Application.QuerySide.Filters;
using CalendarAPI.Application.QuerySide.Queries.GetAllCalendarEvents;
using CalendarAPI.Application.QuerySide.Queries.GetAllCalendarEventsFilter;
using CalendarAPI.Application.QuerySide.Queries.GetCalendarEventsSortedPerTime;
using CalendarAPI.Application.QuerySide.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarAPI.Web.Controllers
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
        public async Task<ActionResult> RemoveCalendarEvent([FromRoute] int calendarEventId)
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
        public async Task<ActionResult> EditCalendarEvent([FromRoute] int calendarEventId,
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