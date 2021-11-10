using System.Net;
using System.Threading.Tasks;
using HolidaySeason.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HolidaySeason.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayEventController
    {
        private readonly IMediator _mediator;

        public HolidayEventController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{holidayEventId}", Name = "GetHolidayEventByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetHolidayEventById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetHolidayEventById.Response>> GetById([FromRoute]GetHolidayEventById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.HolidayEvent == null)
            {
                return new NotFoundObjectResult(request.HolidayEventId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetHolidayEventsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetHolidayEvents.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetHolidayEvents.Response>> Get()
            => await _mediator.Send(new GetHolidayEvents.Request());
        
        [HttpPost(Name = "CreateHolidayEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateHolidayEvent.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateHolidayEvent.Response>> Create([FromBody]CreateHolidayEvent.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetHolidayEventsPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetHolidayEventsPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetHolidayEventsPage.Response>> Page([FromRoute]GetHolidayEventsPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateHolidayEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateHolidayEvent.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateHolidayEvent.Response>> Update([FromBody]UpdateHolidayEvent.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{holidayEventId}", Name = "RemoveHolidayEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveHolidayEvent.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveHolidayEvent.Response>> Remove([FromRoute]RemoveHolidayEvent.Request request)
            => await _mediator.Send(request);
        
    }
}
