using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Data.Entities
{
    public class Chroma
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Price { get; set; }

        //navigation properties
        public Skin Skin { get; set; }


        public Chroma(Chroma chroma)
        {
            Name = chroma.Name;
            URL = chroma.URL;
            Price = chroma.Price;
            Skin = chroma.Skin;
        }

        public Chroma()
        {
        }
    }
}
