using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using HolidaySeason.Api.Extensions;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;
using HolidaySeason.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HolidaySeason.Api.Features
{
    public class GetHolidayEventsPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<HolidayEventDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IHolidaySeasonDbContext _context;
        
            public Handler(IHolidaySeasonDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from holidayEvent in _context.HolidayEvents
                    select holidayEvent;
                
                var length = await _context.HolidayEvents.CountAsync();
                
                var holidayEvents = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = holidayEvents
                };
            }
            
        }
    }
}
