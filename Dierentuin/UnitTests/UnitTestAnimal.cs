using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dierentuin.Models;

namespace UnitTests
{
    public class UnitTestAnimal
    {
        [Fact]
        public void Sunset_DiurnalAnimal_ShouldGoingToSleep()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Diurnal };

            var result = animal.Sunset();

            Assert.Equal("Going to sleep", result);
        }

        [Fact]
        public void Sunset_NocturnalAnimal_ShouldWakingUp()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Nocturnal };

            var result = animal.Sunset();

            Assert.Equal("Waking up", result);
        }

        [Fact]
        public void Sunset_CathermeralAnimal_ShouldBeAlwaysActive()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Cathermeral };

            var result = animal.Sunset();

            Assert.Equal("Always active", result);
        }

        [Fact]
        public void Sunrise_DiurnalAnimal_ShouldGoingToSleep()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Diurnal };

            var result = animal.Sunrise();

            Assert.Equal("Waking up", result);
        }

        [Fact]
        public void Sunrise_NocturnalAnimal_ShouldWakingUp()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Nocturnal };

            var result = animal.Sunrise();

            Assert.Equal("Going to sleep", result);
        }

        [Fact]
        public void Sunrise_CathermeralAnimal_ShouldBeAlwaysActive()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Cathermeral };

            var result = animal.Sunrise();

            Assert.Equal("Always active", result);
        }

        [Fact]
        public void FeedingTime_AnimalWithPreyInEnclosure_ShouldEatPrey()
        {
            Category felis = new Category();
            Enclosure kooi = new Enclosure();
            Animal prey = new Animal() { Name = "prey", Category = felis, Enclosure = kooi };
            Animal predator = new Animal() { Name = "predator", Prey = felis, Enclosure = kooi };

            var result = predator.FeedingTime();

            Assert.Equal("Eats prey", result);
        }

        [Fact]
        public void FeedingTime_AnimalWithNoPreyInEnclosure_ShouldEatGivenFood()
        {
            Category felis = new Category();
            Category canis = new Category();
            Enclosure kooi = new Enclosure();
            Animal notPrey = new Animal() { Name = "notPrey", Category = canis };
            Animal predator = new Animal() { Name = "predator", Prey = felis, Enclosure = kooi };

            var result = predator.FeedingTime();

            Assert.Equal("Eats given food", result);
        }

        [Fact]
        public void FeedingTime_AnimalAloneInCage_ShouldEatGivenFood()
        {
            Category felis = new Category();
            Enclosure kooi = new Enclosure();
            Animal predator = new Animal() { Name = "predator", Prey = felis, Enclosure = kooi };

            var result = predator.FeedingTime();

            Assert.Equal("Eats given food", result);
        }
    }
}