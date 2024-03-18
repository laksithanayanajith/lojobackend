using System.ComponentModel.DataAnnotations;

namespace lojobackend.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        public string SizeName { get; set; } = string.Empty;

        public int ItemId { get; set; }

        public Item? Item { get; set; }
    }
}
