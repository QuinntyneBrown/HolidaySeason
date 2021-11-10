using System;
using HolidaySeason.Api.Models;

namespace HolidaySeason.Api.Features
{
    public static class DigitalAssetExtensions
    {
        public static DigitalAssetDto ToDto(this DigitalAsset digitalAsset)
        {
            return new ()
            {
                DigitalAssetId = digitalAsset.DigitalAssetId
            };
        }
        
    }
}
