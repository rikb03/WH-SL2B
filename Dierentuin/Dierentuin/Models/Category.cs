using Dierentuin.Controllers;
using Microsoft.CodeAnalysis.CSharp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dierentuin.Models;

namespace Dierentuin.Models
{
    [Table("categories")]
    public class Category
    {
        // Database things
        [Key]
        public int Id { get; set; } // Primary key id

        [StringLength(255)]
        [Required]
        public string Name { get; set; } // Name of the category

        [StringLength(255)]
        [Required]
        public string Description { get; set; } // Description of the category

        public virtual ICollection<Animal> Animals { get; set; }  // List of animals in the category
    }
}
