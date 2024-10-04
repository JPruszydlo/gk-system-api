namespace gk_system_api.Entities
{
    public class OfferPlanParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public int OfferPlanId { get; set; }
        public OfferPlan OfferPlan { get; set; }
    }
}
