namespace gk_system_api.Models
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public long CreateAt { get; set; }
        public List<OfferPlanViewModel> OfferPlans { get; set; }
        public List<OfferParamsViewModel> OfferParams { get; set; }
        public List<OfferVisualisationsViewModel> OfferVisualisations { get; set; }
    }
}
