using NuGet.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using static Dierentuin.Models.Enclosure;

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
            Cathemeral
        };

        // Database fields
        [Key]
        public int Id { get; set; } // Primary key id

        [StringLength(255)]
        [Required]
        public string Name { get; set; } // Name of the animal

        [StringLength(255)]
        [Required]
        public string Species { get; set; } // Species of the animal

        [Column("categories_id")]
        [AllowNull]
        [ForeignKey("Category")]
        public int? CategoryId { get; set; } // Id for the animal's category

        [Required]
        [EnumDataType(typeof(SizeType))]
        public SizeType Size { get; set; } // Size of the animal

        [Required]
        [EnumDataType(typeof(DietaryClassType))]
        public DietaryClassType Dietary { get; set; } // Dietary need of the animal

        [Required]
        [EnumDataType(typeof(ActivityPatternType))]
        public ActivityPatternType ActivityPattern { get; set; } // Activity pattern of the animal

        [AllowNull]
        public int? Prey { get; set; } // Prey of the animal

        [Column("enclosures_id")]
        [AllowNull]
        [ForeignKey("Enclosure")]
        public int? EnclosureId { get; set; } // Id for the animal's enclosure

        [Column("spaceRequirement")]
        [Required]
        public double SpaceRequirement { get; set; } // The animal's required space in square meters

        [Required]
        [EnumDataType(typeof(SecurityLevelType))]
        public SecurityLevelType SecurityRequirement { get; set; } // The required security of the animal's enclosure

        // Foreign table data
        public Category Category { get; set; } // Animal's category. From categories where id = categories_id
        public Enclosure Enclosure { get; set; } // Animal's enclosure. From enclosures where id = enclosures_id

        // Method to determine the activity status at sunrise
        public string Sunrise()
        {
            return ActivityPattern switch
            {
                ActivityPatternType.Diurnal => $"{Name} wakes up at sunrise.",
                ActivityPatternType.Nocturnal => $"{Name} goes to sleep at sunrise.",
                ActivityPatternType.Cathemeral => $"{Name} is active throughout the day and night.",
                _ => throw new NotImplementedException()
            };
        }

        // Method to determine the activity status at sunset
        public string Sunset()
        {
            return ActivityPattern switch
            {
                ActivityPatternType.Diurnal => $"{Name} goes to sleep at sunset.",
                ActivityPatternType.Nocturnal => $"{Name} wakes up at sunset.",
                ActivityPatternType.Cathemeral => $"{Name} is active throughout the day and night.",
                _ => throw new NotImplementedException()
            };
        }

        // Method to determine the feeding time
        public string FeedingTime(Enclosure enclosure, Category? category)
        {
            if (category != null)
            {
                foreach (Animal animal in enclosure.Animals)
                {
                    if (animal.Category == category)
                    {
                        return $"{Name} eats {Prey}.";
                    }
                }
            }
            return Dietary switch
            {
                DietaryClassType.Carnivore => $"{Name} eats meat.",
                DietaryClassType.Herbivore => $"{Name} eats plants.",
                DietaryClassType.Omnivore => $"{Name} eats both plants and meat.",
                DietaryClassType.Insectivore => $"{Name} eats insects.",
                DietaryClassType.Piscivore => $"{Name} eats fish.",
                _ => throw new NotImplementedException()
            };
        }

        // Method to check constraints
        public string CheckConstraint(Enclosure enclosure)
        {
            double totalSpaceRequired = 0;
            bool isSpaceSufficient = false; // Example constraint

            foreach (Animal animal in enclosure.Animals)
            {
                totalSpaceRequired += animal.SpaceRequirement;
            };

            if (totalSpaceRequired > enclosure.Size)
            {
                isSpaceSufficient = false;
            }
            else
            {
                isSpaceSufficient = true;
            }

            // Placeholder example to check some constraints
            bool isSecurityAdequate = SecurityRequirement == enclosure.SecurityLevel; // Example constraint

            return $"Space sufficient: {isSpaceSufficient}, Security adequate: {isSecurityAdequate}";
        }
    }
}