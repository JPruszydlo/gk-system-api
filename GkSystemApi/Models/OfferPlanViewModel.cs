namespace gk_system_api.Models
{
    public class OfferPlanViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FloorName { get; set; }
        public List<OfferPlanParamsViewModel> OfferPlanParams { get; set; }
    }
}
