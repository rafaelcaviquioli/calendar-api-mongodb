using System;
using System.Net;
using System.Threading.Tasks;
using CalendarAPI.Application.CommandSide.Commands.AddNewEvent;
using CalendarAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarAPI.Controllers
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
        public async Task<ActionResult> EditCalendarEvent([FromRoute] int calendarEventId, [FromBody] EditCalendarEventCommand command)
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
    }
}