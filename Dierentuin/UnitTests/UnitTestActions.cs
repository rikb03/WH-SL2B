using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dierentuin.Models;

namespace UnitTests
{
    public class UnitTestActions
    {
        [Fact]
        public void Sunset_DiurnalAnimal_ShouldGoingToSleep()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Diurnal};

            var result = animal.Sunset();

            Assert.Equal("Going to sleep", result);
        }

        public void Sunset_NocturnalAnimal_ShouldWakingUp()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Nocturnal };

            var result = animal.Sunset();

            Assert.Equal("Waking up", result);
        }

        public void Sunset_CathermeralAnimal_ShouldBeAlwaysActive()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Cathermeral };

            var result = animal.Sunset();

            Assert.Equal("Always active", result);
        }

        public void Sunrise_DiurnalAnimal_ShouldGoingToSleep()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Diurnal };

            var result = animal.Sunrise();

            Assert.Equal("Waking up", result);
        }

        public void Sunrise_NocturnalAnimal_ShouldWakingUp()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Nocturnal };

            var result = animal.Sunrise();

            Assert.Equal("Going to sleep", result);
        }

        public void Sunrise_CathermeralAnimal_ShouldBeAlwaysActive()
        {
            Animal animal = new Animal() { ActivityPattern = Animal.ActivityPatternType.Cathermeral };

            var result = animal.Sunrise(); 

            Assert.Equal("Always active", result);
        }
    }
}