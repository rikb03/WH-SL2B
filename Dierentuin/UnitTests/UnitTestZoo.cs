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
    }
}
