using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public string ImageMimeType { get; set; }
    }
}
