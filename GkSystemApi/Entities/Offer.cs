namespace gk_system_api.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public long CreateAt{ get; set; }
        public List<OfferPlan> OfferPlans { get; set; }
        public List<OfferParams> OfferParams { get; set; }
        public List<OfferVisualisations> OfferVisualisations { get; set; }
    }
}
