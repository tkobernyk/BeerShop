using Microsoft.VisualStudio.TestTools.UnitTesting;

using BeerShop.DataStore.Models;

namespace BeerShop.DataStore.Test
{
    [TestClass]
    public class BreweryModelTest : EntityTest
    {
        public BreweryModelTest() { }
        [TestMethod]
        [TestCategory("DataStore.Models.Validation")]
        public void BreweryNameRequired()
        {
            var brewery = new Brewery { Id = 1 };
            Assert.IsTrue(ValidateModel(brewery).Count > 0);
        }

        [TestMethod]
        [TestCategory("DataStore.Models.Validation")]
        public void BreweryNameMaxLengthRequired()
        {
            var beer = new Brewery { Id = 1, Name = "123456789012345678901" };
            Assert.IsTrue(ValidateModel(beer).Count > 0);
        }
    }
}
