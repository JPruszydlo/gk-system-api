namespace gk_system_api.Entities
{
    public class OfferParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }

    }
}
