using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Data.Entities;

public class Session
{
    [Key]
    public required string Id { get; set; }
    public required User User { get; set; }
}