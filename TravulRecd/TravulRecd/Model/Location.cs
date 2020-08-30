namespace TravulRecd.Model
{
    public class Location
    {
        public string address { get; set; }
        public string crossStreet { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public int distance { get; set; }
        public string cc { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string[] formattedAddress { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
    }
}


