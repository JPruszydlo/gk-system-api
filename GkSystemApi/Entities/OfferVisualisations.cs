namespace gk_system_api.Entities
{
    public class OfferVisualisations
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImageByteType{get;set;}
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
