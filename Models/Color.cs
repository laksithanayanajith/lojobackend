using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lojobackend.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ItemId { get; set; }

        public Item? Item { get; set; }
    }
}
