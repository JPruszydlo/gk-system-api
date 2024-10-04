using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gk_system_api.Entities
{
    public class RealisationImage
    {
        [Key]
        public int RealisationImageId { get; set; }
        public string ImageSrc { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImageByteType { get; set; }
        public bool IsFavourite { get; set; }

        public int RealisationId { get;set; }

        [ForeignKey("RealisationId")]
        public Realisation Realisation { get; set; }
    }
}
