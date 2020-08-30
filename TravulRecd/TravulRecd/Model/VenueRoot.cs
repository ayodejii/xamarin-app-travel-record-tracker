using System;
using TravulRecd.Helpers;

namespace TravulRecd.Model
{
    public class VenueRoot
    {
        public Response response { get; set; }
        public static string GenerateUrl(double latitude, double longitude)
        {
            string url = string.Format(Constants.VENUE_SEARCH,
                                       latitude,
                                       longitude,
                                       Constants.CLIENT_ID,
                                       Constants.CLIENT_SECRET,
                                       DateTime.Now.ToString("yyyyMMdd"));
            return url;
        }
    }
}

