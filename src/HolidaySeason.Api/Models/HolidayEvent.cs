using System;
using System.Collections.Generic;

namespace HolidaySeason.Api.Models
{
    public class HolidayEvent
    {
        public Guid HolidayEventId { get; private set; }
        public DateTime Date { get; private set; }
        public List<Guest> Guests { get; private set; }
    }
}
