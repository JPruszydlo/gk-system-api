namespace gk_system_api.Models
{
    public class RealisationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long CratedAt { get; set; }
        public List<RealisationImageViewModel> RealisationImages { get; set; }
    }
}
