using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis.Models
{
    public class AnimalCategory
    {
        [Key]
        public int IdAnimalCategory { get; set; }

        public string NameAnimalCategory { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is AnimalCategory)) return false;
            var compObj = obj as AnimalCategory;
            return this.NameAnimalCategory == compObj.NameAnimalCategory;
        }

        public override string ToString()
        {
            return NameAnimalCategory;
        }
    }
}
