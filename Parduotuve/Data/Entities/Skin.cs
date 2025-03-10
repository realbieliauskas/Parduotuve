namespace Parduotuve.Data.Entities
{
    public class Skin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ChampionName { get; set; }
        public string? SplashUrl { get; set; }
        public string? CinematicSplashUrl { get; set; }
        public double? Price { get; set; }
        public string? Quote { get; set; }
        // navigation properties
        public ICollection<Chroma> ChromaList { get; set; }
        public string? Chromas { get; set; }
        public string? ChromaURLs { get; set; }
        public string? ChromaPrices { get; set; }
    }
}
