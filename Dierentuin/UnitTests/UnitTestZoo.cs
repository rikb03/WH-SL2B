using Dierentuin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class UnitTestZoo
    {
        [Fact]
        public void Sunset_MixedActivityPatterns_ShouldReturnCorrectAction()
        {
            Zoo zoo = new Zoo()
            {
                Animals = new List<Animal>
                {
                    new Animal { Name = "Kees", ActivityPattern = Animal.ActivityPatternType.Diurnal },
                    new Animal { Name = "Piet", ActivityPattern = Animal.ActivityPatternType.Nocturnal },
                    new Animal { Name = "Jan", ActivityPattern = Animal.ActivityPatternType.Cathermeral }
                }
            };

            var result = zoo.Sunset();

            Assert.Equal("Kees is going to sleep", result["Kees"]);
            Assert.Equal("Piet is waking up", result["Piet"]);
            Assert.Equal("Jan is always active", result["Jan"]);
        }

        [Fact]
        public void Sunset_EmptyZoo_ShouldReturnNull()
        {
            Zoo zoo = new Zoo();

            var result = zoo.Sunset();

            Assert.Null(result);
        }

        [Fact]
        public void Sunrise_MixedActivityPatterns_ShouldReturnCorrectAction()
        {
            Zoo zoo = new Zoo()
            {
                Animals = new List<Animal>
                {
                    new Animal { Name = "Kees", ActivityPattern = Animal.ActivityPatternType.Diurnal },
                    new Animal { Name = "Piet", ActivityPattern = Animal.ActivityPatternType.Nocturnal },
                    new Animal { Name = "Jan", ActivityPattern = Animal.ActivityPatternType.Cathermeral }
                }
            };

            var result = zoo.Sunset();

            Assert.Equal("Kees is waking up", result["Kees"]);
            Assert.Equal("Piet is going to sleep", result["Piet"]);
            Assert.Equal("Jan is always active", result["Jan"]);
        }

        [Fact]
        public void Sunrise_EmptyZoo_ShouldReturnNull()
        {
            Zoo zoo = new Zoo();

            var result = zoo.Sunset();

            Assert.Null(result);
        }

        [Fact]
        public void FeedingTime_AnimalsWithPreyInEnclosure_ShouldEatPrey()
        {
            Zoo zoo = new Zoo();
            Category felines = new Category();
            Category canines = new Category();
            Enclosure kooi = new Enclosure()
            {
                Animal = new List<Animal>
                {
                    new Animal { Name = "Kees"},
                    new Animal { Name = "Piet", Prey = canines, Category = felines },
                    new Animal { Name = "Jan", Category = canines}
                }
            };

            Enclosure hok = new Enclosure()
            {
                Animal = new List<Animal>
                {
                    new Animal { Name = "Hennie", Category = felines, Prey = canines },
                    new Animal { Name = "Gert", Category = canines }

                }
            };

            var result = zoo.FeedingTime();

            Assert.Equal("Kees eats given food", result["Kees"]);
            Assert.Equal("Piet eats Jan", result["Piet"]);
            Assert.Null(result["Jan"]); // Jan is eaten :(
            Assert.Equal("Hennie eats Gert", result["Hennie"]);
            Assert.Null(result["Gert"]); // Gert is eaten :'(
        }

        [Fact]
        public void FeedingTime_AnimalsWithNoPreyInEnclosure_NoneShouldEatPrey()
        {
            Zoo zoo = new Zoo();
            Category felines = new Category();
            Category canines = new Category();
            Enclosure kooi = new Enclosure()
            {
                Animal = new List<Animal>
                {
                    new Animal { Name = "Piet", Category = felines, Prey = canines },
                    new Animal { Name = "Jan", Category = felines }
                }
            };

            Enclosure hok = new Enclosure()
            {
                Animal = new List<Animal>
                {
                    new Animal { Name = "Hennie", Category = canines, Prey = felines },
                    new Animal { Name = "Gert", Category = canines }

                }
            };

            var result = zoo.FeedingTime();

            Assert.Equal("Piet eats given food", result["Piet"]);
            Assert.Equal("Jan eats given food", result["Jan"]);
            Assert.Equal("Hennie eats given food", result["Hennie"]);
            Assert.Equal("Gert eats given food", result["Gert"]);
        }

        [Fact]
        public void FeedingTime_EmptyEnclosure_ShouldReturnNull()
        {
            Zoo zoo = new Zoo();
            Enclosure kooi = new Enclosure();
            Enclosure hok = new Enclosure();

            var result = zoo.FeedingTime();

            Assert.Null(result);
        }
    }
}
