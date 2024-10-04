using System.Reflection.Metadata;

namespace gk_system_api.Entities
{
    public class CarouselConfig
    {
        public int Id { get; set; }
        public string SubPage { get; set; }
        public string Image { get; set; }
        public string ContentText { get; set; }
        public string ContentTitle { get; set; }
        public byte[] ByteImage { get; set; }
        public string ByteImageType { get; set; }
    }
}
