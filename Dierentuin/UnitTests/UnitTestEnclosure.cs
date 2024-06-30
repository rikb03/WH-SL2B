using Dierentuin.Models;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class UnitTestEnclosure
    {
        [Fact]
        public void Sunset_MixedActivityPatterns_ShouldReturnCorrectAction()
        {
            Enclosure enclosure = new Enclosure()
            {
                Animals = new List<Animal>
                {
                    new Animal { Name = "Kees", ActivityPattern = Animal.ActivityPatternType.Diurnal },
                    new Animal { Name = "Piet", ActivityPattern = Animal.ActivityPatternType.Nocturnal },
                    new Animal { Name = "Jan", ActivityPattern = Animal.ActivityPatternType.Cathemeral }
                }
            };

            var result = enclosure.Sunset();

            Assert.Equal("Kees is going to sleep", result["Kees"]);
            Assert.Equal("Piet is waking up", result["Piet"]);
            Assert.Equal("Jan is always active", result["Jan"]);
        }

        [Fact]
        public void Sunset_EmptyEnclosure_ShouldReturnNull()
        {
            Enclosure enclosure = new Enclosure();

            var result = enclosure.Sunset();

            Assert.Null(result);
        }

        [Fact]
        public void Sunrise_MixedActivityPatterns_ShouldReturnCorrectAction()
        {
            Enclosure enclosure = new Enclosure()
            {
                Animals = new List<Animal>
                {
                    new Animal { Name = "Kees", ActivityPattern = Animal.ActivityPatternType.Diurnal },
                    new Animal { Name = "Piet", ActivityPattern = Animal.ActivityPatternType.Nocturnal },
                    new Animal { Name = "Jan", ActivityPattern = Animal.ActivityPatternType.Cathemeral }
                }
            };

            var result = enclosure.Sunset();

            Assert.Equal("Kees is waking up", result["Kees"]);
            Assert.Equal("Piet is going to sleep", result["Piet"]);
            Assert.Equal("Jan is always active", result["Jan"]);
        }

        [Fact]
        public void Sunrise_EmptyEnclosure_ShouldReturnNull()
        {
            Enclosure enclosure = new Enclosure();

            var result = enclosure.Sunset();

            Assert.Null(result);
        }

        [Fact]
        public void FeedingTime_AnimalsWithPreyInEnclosure_ShouldEatPrey()
        {
            Category felines = new Category(){ Id = 2 };
            Category canines = new Category(){ Id = 1 };
            Enclosure enclosure = new Enclosure()
            {
                Animals = new List<Animal>
                {
                    new Animal { Name = "Kees"},
                    new Animal { Name = "Piet", Prey = canines.Id, Category = felines },
                    new Animal { Name = "Jan", Category = canines}
                }
            };

            var result = enclosure.FeedingTime();

            Assert.Equal("Kees eats given food", result["Kees"]);
            Assert.Equal("Piet eats Jan", result["Piet"]);
            Assert.Null(result["Jan"]); // Jan is eaten :(
        }

        [Fact]
        public void FeedingTime_AnimalsWithNoPreyInEnclosure_NoneShouldEatPrey()
        {
            Category felines = new Category(){ Id = 2 };
            Category canines = new Category(){ Id = 1 };
            Enclosure enclosure = new Enclosure()
            {
                Animals = new List<Animal>
                {
                    new Animal { Name = "Piet", Category = felines, Prey = canines.Id },
                    new Animal { Name = "Jan", Category = felines }
                }
            };

            var result = enclosure.FeedingTime();

            Assert.Equal("Piet eats given food", result["Piet"]);
            Assert.Equal("Jan eats given food", result["Jan"]);
        }

        [Fact]
        public void FeedingTime_EmptyEnclosure_ShouldReturnNull()
        {
            Enclosure enclosure = new Enclosure();

            var result = enclosure.FeedingTime();

            Assert.Null(result);
        }
    }
}
