using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;

namespace TravulRecd.Logic
{
    public class VenueLogic
    {
        public static async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var venues = new List<Venue>();
            string url = VenueRoot.GenerateUrl(latitude, longitude);

            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(result);

                venues = venueRoot.response.venues;
            }
            return venues;
        }
    }
}
