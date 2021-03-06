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
    public class GetDigitalAssets
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<DigitalAssetDto> DigitalAssets { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IHolidaySeasonDbContext _context;
        
            public Handler(IHolidaySeasonDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    DigitalAssets = await _context.DigitalAssets.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
