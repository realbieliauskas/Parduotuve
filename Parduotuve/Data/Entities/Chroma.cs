using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Data.Entities;

public class Chroma : IEquatable<Chroma>
{
    public Chroma(Chroma chroma)
    {
        Name = chroma.Name;
        Url = chroma.Url;
        Price = chroma.Price;
        Skin = chroma.Skin;
    }

    public Chroma()
    {
    }

    [Key] public int Id { get; set; }

    public string Name { get; set; }
    public string Url { get; set; }
    public string Price { get; set; }

    //navigation properties
    public Skin Skin { get; set; }

    public bool Equals(Chroma? other)
    {
        return other != null &&
            Name == other.Name &&
            Url == other.Url &&
            Price == other.Price &&
            Skin.Id == other.Skin.Id;
    }
}