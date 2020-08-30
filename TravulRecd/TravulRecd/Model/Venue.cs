using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TravulRecd.Model
{
    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public List<Category> categories { get; set; }

        internal static async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var venues = new List<Venue>();
            string url = VenueRoot.GenerateUrl(latitude, longitude);

            var response = await App.http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(result);

            //var venuesList = venueRoot.response.venues.Where(x => 
            //(x.categories.Where(c => string.IsNullOrEmpty(c.id)) != null)).ToList();
            var venueList = venueRoot.response.venues.ToList();
            foreach (var venue in venueList)
            {
                if (venue.categories.Count > 0)
                    venues.Add(venue);
            }
            return venues;
        }
    }
}

