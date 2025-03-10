using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Data.Entities
{
    public class Chroma
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Price { get; set; }

        //navigation properties
        public Skin Skin { get; set; }
    }
}
