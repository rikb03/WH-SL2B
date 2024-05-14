using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using static Dierentuin.Models.Animal;

namespace Dierentuin.Models
{
    [Table("enclosures")]
    public class Enclosure
    {
        // Enums
        public enum ClimateType // Climate enum
        { 
            Tropical, 
            Temperate, 
            Arctic 
        };
        public enum HabitatType // Habitat enum
        { 
            Forest, 
            Aquatic, 
            Desert, 
            Grassland 
        };
        public enum SecurityLevelType // Security level enum
        { 
            Low, 
            Medium, 
            High 
        };

        // Database things
        [Key]
        public int Id { get; set; } // Primary key id

        [StringLength(255)]
        [Required]
        public string Name { get; set; } // Name of the enclosure

        //[Required]
        public List<Animal> Animal { get; set; } // List of animals in the enclosure

        [Required]
        [EnumDataType(typeof(ClimateType))]
        public ClimateType climate {  get; set; } // The climate of the enclosure

        [Required]
        [EnumDataType(typeof(HabitatType))]
        public HabitatType habitat { get; set;} // Habitat type of the enclosure

        [Required]
        [EnumDataType(typeof(SecurityLevelType))]
        public SecurityLevelType securityLevel { get; set; } // Security level of the enclosure

        [Required]
        public double Size { get; set; } // Size of the enclosure in square meters
    }
}
