namespace gk_system_api.Entities
{
    public class Realisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long CratedAt { get; set; }
        public List<RealisationImage> RealisationImages { get; set; }
    }
}
