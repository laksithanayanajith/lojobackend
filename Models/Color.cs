using System.ComponentModel.DataAnnotations;

namespace lojobackend.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ColorId { get; set; }

        public Item? Item { get; set; }
    }
}
