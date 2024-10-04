namespace gk_system_api.Models
{
    public class Localisation
    {
        public int Id { get; set; }
        public string IPv4 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CountryName{ get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
