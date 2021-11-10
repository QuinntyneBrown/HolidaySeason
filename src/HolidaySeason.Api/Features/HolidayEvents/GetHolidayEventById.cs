using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidaySeason.Api.Features
{
    public class GetHolidayEventById
    {
        public class Request: IRequest<Response>
        {
            public Guid HolidayEventId { get; set; }
        }

        public class Response: ResponseBase
        {
            public HolidayEventDto HolidayEvent { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IHolidaySeasonDbContext _context;
        
            public Handler(IHolidaySeasonDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    HolidayEvent = (await _context.HolidayEvents.SingleOrDefaultAsync(x => x.HolidayEventId == request.HolidayEventId)).ToDto()
                };
            }
            
        }
    }
}
