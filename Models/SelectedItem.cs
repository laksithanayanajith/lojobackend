using System.ComponentModel.DataAnnotations;

namespace lojobackend.Models
{
    public class SelectedItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; } = 0.00;

        public int ItemId { get; set; }

        public Item? Item { get; set; }
    }
}
