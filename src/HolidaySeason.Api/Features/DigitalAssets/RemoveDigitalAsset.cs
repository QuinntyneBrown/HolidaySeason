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
    public class RemoveDigitalAsset
    {
        public class Request: IRequest<Response>
        {
            public Guid DigitalAssetId { get; set; }
        }

        public class Response: ResponseBase
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IHolidaySeasonDbContext _context;
        
            public Handler(IHolidaySeasonDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var digitalAsset = await _context.DigitalAssets.SingleAsync(x => x.DigitalAssetId == request.DigitalAssetId);
                
                _context.DigitalAssets.Remove(digitalAsset);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    DigitalAsset = digitalAsset.ToDto()
                };
            }
            
        }
    }
}
