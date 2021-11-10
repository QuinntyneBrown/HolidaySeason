using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using HolidaySeason.Api.Models;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;

namespace HolidaySeason.Api.Features
{
    public class RemoveHolidayEvent
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
                var holidayEvent = await _context.HolidayEvents.SingleAsync(x => x.HolidayEventId == request.HolidayEventId);
                
                _context.HolidayEvents.Remove(holidayEvent);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    HolidayEvent = holidayEvent.ToDto()
                };
            }
            
        }
    }
}
