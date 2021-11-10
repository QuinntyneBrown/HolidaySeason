using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidaySeason.Api.Features
{
    public class GetStoredEventById
    {
        public class Request: IRequest<Response>
        {
            public Guid StoredEventId { get; set; }
        }

        public class Response: ResponseBase
        {
            public StoredEventDto StoredEvent { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IHolidaySeasonDbContext _context;
        
            public Handler(IHolidaySeasonDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    StoredEvent = (await _context.StoredEvents.SingleOrDefaultAsync(x => x.StoredEventId == request.StoredEventId)).ToDto()
                };
            }
            
        }
    }
}
