using gk_system_api.Utils;

namespace gk_system_api.Entities
{
    public class GeneralConfig
    {
        public int Id { get; set; }
        public ConfigGroup ConfigGroup { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public byte[] Image { get; set; }
        public string ImageType { get; set; }
    }
}
