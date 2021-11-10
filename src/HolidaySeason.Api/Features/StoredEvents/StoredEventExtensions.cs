using System;
using HolidaySeason.Api.Models;

namespace HolidaySeason.Api.Features
{
    public static class StoredEventExtensions
    {
        public static StoredEventDto ToDto(this StoredEvent storedEvent)
        {
            return new ()
            {
                StoredEventId = storedEvent.StoredEventId
            };
        }
        
    }
}
