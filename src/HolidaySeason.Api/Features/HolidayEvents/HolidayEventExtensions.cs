using System;
using HolidaySeason.Api.Models;

namespace HolidaySeason.Api.Features
{
    public static class HolidayEventExtensions
    {
        public static HolidayEventDto ToDto(this HolidayEvent holidayEvent)
        {
            return new ()
            {
                HolidayEventId = holidayEvent.HolidayEventId
            };
        }
        
    }
}
