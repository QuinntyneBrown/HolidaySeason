using System.Net;
using System.Threading.Tasks;
using HolidaySeason.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HolidaySeason.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DigitalAssetController
    {
        private readonly IMediator _mediator;

        public DigitalAssetController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{digitalAssetId}", Name = "GetDigitalAssetByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDigitalAssetById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDigitalAssetById.Response>> GetById([FromRoute]GetDigitalAssetById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.DigitalAsset == null)
            {
                return new NotFoundObjectResult(request.DigitalAssetId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetDigitalAssetsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDigitalAssets.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDigitalAssets.Response>> Get()
            => await _mediator.Send(new GetDigitalAssets.Request());
        
        [HttpPost(Name = "CreateDigitalAssetRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateDigitalAsset.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateDigitalAsset.Response>> Create([FromBody]CreateDigitalAsset.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetDigitalAssetsPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDigitalAssetsPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDigitalAssetsPage.Response>> Page([FromRoute]GetDigitalAssetsPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateDigitalAssetRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateDigitalAsset.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateDigitalAsset.Response>> Update([FromBody]UpdateDigitalAsset.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{digitalAssetId}", Name = "RemoveDigitalAssetRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveDigitalAsset.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveDigitalAsset.Response>> Remove([FromRoute]RemoveDigitalAsset.Request request)
            => await _mediator.Send(request);
        
    }
}
