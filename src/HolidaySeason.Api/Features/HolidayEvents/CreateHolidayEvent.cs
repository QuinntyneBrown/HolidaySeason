using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HolidaySeason.Api.Models;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;

namespace HolidaySeason.Api.Features
{
    public class CreateHolidayEvent
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.HolidayEvent).NotNull();
                RuleFor(request => request.HolidayEvent).SetValidator(new HolidayEventValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public HolidayEventDto HolidayEvent { get; set; }
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
                var holidayEvent = new HolidayEvent();
                
                _context.HolidayEvents.Add(holidayEvent);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    HolidayEvent = holidayEvent.ToDto()
                };
            }
            
        }
    }
}
