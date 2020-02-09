using System.Threading.Tasks;
using CalendarAPI.Application.CommandSide.Commands.AddNewEvent;
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
        public async Task<int> AddNewEvent([FromBody] AddNewEventCommand command)
        {
            var id = await _mediator.Send(command);

            return id;
        }
    }
}