using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Data.Entities
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public User? User { get; set; }
        public bool IsCompleted { get; set; }
    }
}
