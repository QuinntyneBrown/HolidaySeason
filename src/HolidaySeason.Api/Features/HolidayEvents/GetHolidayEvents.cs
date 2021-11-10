using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidaySeason.Api.Features
{
    public class GetHolidayEvents
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<HolidayEventDto> HolidayEvents { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IHolidaySeasonDbContext _context;
        
            public Handler(IHolidaySeasonDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    HolidayEvents = await _context.HolidayEvents.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
