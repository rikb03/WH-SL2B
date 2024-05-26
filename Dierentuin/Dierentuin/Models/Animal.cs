using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dierentuin.Models
{
    [Table("animals")]
    public class Animal
    {
        // Enums
        public enum SizeType // Sizes enum
        {
            Microscopic, 
            VerySmall,
            Small,
            Medium,
            Large, 
            VeryLarge
        };
        public enum DietaryClassType // Dietary enum
        {
            Carnivore,
            Herbivore, 
            Omnivore, 
            Insectivore, 
            Piscivore 
        };
        public enum ActivityPatternType // ActivityPattern enum
        { 
            Diurnal, 
            Nocturnal, 
            Cathermeral 
        };
        public enum SecurityRequirementType // Security enum
        { 
            Low, 
            Medium, 
            High 
        };

        // Database things
        [Key]
        public int Id { get; set; } // Pimary key id

        [StringLength(255)]
        [Required]
        public string Name { get; set; } // Name of the animal

        [StringLength(255)]
        [Required]
        public string Species { get; set; } // Specie of the animal

        [Column("categories_id")]
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; } // Id for the animals category

        [Required]
        [EnumDataType(typeof(SizeType))]
        public SizeType Size { get; set; } // Size of the animal

        [EnumDataType(typeof(DietaryClassType))]
        public DietaryClassType Dietary { get; set;} // Dietary need of the animal

        [EnumDataType(typeof(ActivityPatternType))]
        public ActivityPatternType ActivityPattern { get; set; } // Activity pattern of the animal

        [StringLength(255)]
        [Required]
        public Category Prey { get; set; } // Prey of the animal

        [Column("enclosures_id")]
        [Required]
        [ForeignKey("Enclosure")]
        public int EnclosureId { get; set; } // Id for the animals enclosure

        [Column("spaceRequirement")]
        [Required]
        public double SpaceRequirement { get; set; } // The animals required space in square meters

        [Required]
        [EnumDataType(typeof(SecurityRequirementType))]
        public SecurityRequirementType SecurityRequirement { get; set;} // The required security of the animals enclosure

        [StringLength(255)]
        public string? ImagePath { get; set; } // The path to the image of the animal (optional)
        

        // Foreign table data
        public Category Category { get; set; } // Animals category. From categories where id = categories_id

        public Enclosure Enclosure { get; set; } // Animals enclosure. From enclosures where id = enclosures_id

        public string Sunset()
        {
            throw new NotImplementedException();
        }

        public string Sunrise()
        {
            throw new NotImplementedException();
        }

        public string FeedingTime()
        {
            throw new NotImplementedException();
        } 
    }
}
