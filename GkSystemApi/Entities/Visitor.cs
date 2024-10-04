using gk_system_api.Models;

namespace gk_system_api.Entities
{
    public class Visitor
    {
        public int Id { get; set; }
        public string FingerPrint { get; set; }
        public Localisation Localisation { get; set; }
    }
}
