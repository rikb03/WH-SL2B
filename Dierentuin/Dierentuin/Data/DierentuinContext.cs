using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Models;
using Bogus;

namespace Dierentuin.Data
{
    public class DierentuinContext : DbContext
    {
        public DierentuinContext (DbContextOptions<DierentuinContext> options)
            : base(options)
        {
        }

        public DbSet<Dierentuin.Models.Animal> Animal { get; set; } = default!;
        public DbSet<Dierentuin.Models.Category> Category { get; set; }
        public DbSet<Dierentuin.Models.Enclosure> Enclosure { get; set; }

        // Lists for seeding data
        public static List<Enclosure> Enclosures;
        public static List<Category> Categories;
        public static List<Animal> Animals;

        private bool s_seeded = false; // Boolean to check if db is already seeded

        List<string> enclosureNames = new List<string> // List with enclosure names made by ChatGPT
        {
            "Kooi",
            "Sanctuary",
            "Pool",
            "House",
            "Savanna",
            "Aviary",
            "Island",
            "Territory",
            "Cave",
            "Outback",
            "Pavilion",
            "Enclosure",
            "Habitat",
            "Reserve",
            "Garden",
            "Dome",
            "Paddock",
            "Pen",
            "Grove",
            "Retreat"
        };

        List<string> categoryNames = new List<string> // List with animal categories made by ChatGPT
        {
            "Felines",
            "Canines",
            "Primates",
            "Reptiles",
            "Birds",
            "Amphibians",
            "Insects",
            "Aquatic Animals",
            "Mammals",
            "Rodents",
            "Marsupials",
            "Ungulates",
            "Cetaceans",
            "Arachnids",
            "Bovines",
            "Pachyderms",
            "Mustelids",
            "Crustaceans",
            "Mollusks",
            "Bats"
        };

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Animals)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Animal>()
                .HasOne(e => e.Enclosure)
                .WithMany(e => e.Animals)
                .HasForeignKey(e => e.EnclosureId);

            Seeder(); // Seeding with bogus faker
            
            // Add the data to the database
            modelBuilder.Entity<Enclosure>().HasData(Enclosures); 
            modelBuilder.Entity<Category>().HasData(Categories);
            modelBuilder.Entity<Animal>().HasData(Animals);


        }

        protected void Seeder()
        {
            if (s_seeded) { return; } // Prevent multiple seeding (OnModelCreatinggets called multiple times)
            s_seeded = true;

            // Amount of %model% you want
            const int numberOfEnclosures = 10;
            const int numberOfCategories = 10;
            const int numberOfAnimals = 10;

            Enclosures = new Faker<Enclosure>()
                .RuleFor(e => e.Id, f => f.IndexFaker + 1)
                .RuleFor(e => e.Name, f => f.PickRandom(enclosureNames))
                .RuleFor(e => e.Climate, f => f.PickRandom<Enclosure.ClimateType>())
                .RuleFor(e => e.Habitat, f => f.PickRandom<Enclosure.HabitatType>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<Enclosure.SecurityLevelType>())
                .RuleFor(e => e.Size, f => f.Random.Double(1, 100))
                .Generate(numberOfEnclosures);

            Categories = new Faker<Category>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.Name, f => f.PickRandom(categoryNames))
                .RuleFor(c => c.Description, f => f.Lorem.Sentence(3))
                .Generate(numberOfCategories);

            Animals = new Faker<Animal>()
                .RuleFor(a => a.Id, f => f.IndexFaker + 1)
                .RuleFor(a => a.Name, f => f.Name.FirstName())
                .RuleFor(a => a.Species, f => f.PickRandom(categoryNames))
                .RuleFor(a => a.CategoryId, f => f.PickRandom(Categories).Id)
                .RuleFor(a => a.Size, f => f.PickRandom<Animal.SizeType>())
                .RuleFor(a => a.Dietary, f => f.PickRandom<Animal.DietaryClassType>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<Animal.ActivityPatternType>())
                .RuleFor(a => a.Prey, f => f.Random.Int(1, numberOfCategories)) // Prey is based on category so the number of categories is the max
                .RuleFor(a => a.EnclosureId, f => f.PickRandom(Enclosures).Id)
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Double(1, 100))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<Enclosure.SecurityLevelType>())
                .Generate(numberOfAnimals);
        }
    }
}
