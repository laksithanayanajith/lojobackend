using System.ComponentModel.DataAnnotations;

namespace lojobackend.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; } = 0.00;

        public string Category { get; set; } = string.Empty;

        public string? DefaultImage { get; set; } = string.Empty;

        public SelectedItem? SelectedItem { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        public ICollection<Image>? Images { get; set; }

        public ICollection<Color>? Colors { get; set; }

        public ICollection<Size>? Sizes { get; set; }
    }
}
