using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Data.Entities;

public class OrderItem
{
    [Key] public int Id { get; set; }

    public string OrderId { get; set; }
    public Skin Skin { get; set; }
    public int Amount { get; set; }
}