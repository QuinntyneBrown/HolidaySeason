using System;
using HolidaySeason.Api.Models;

namespace HolidaySeason.Api.Features
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new ()
            {
                UserId = user.UserId
            };
        }
        
    }
}
