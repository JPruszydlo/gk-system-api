namespace gk_system_api.Entities
{
    public class OfferPlan
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImageByteType { get; set; }
        public string FloorName { get; set; }
        public List<OfferPlanParams> OfferPlanParams { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
