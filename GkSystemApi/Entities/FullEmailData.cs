using gk_system_api.Models;

namespace gk_system_api.Entities
{
    public class FullEmailData
    {
        public int Id { get; set; }
        public Email Email { get; set; }
        public Visitor Visitor{ get; set; }
    }
}
