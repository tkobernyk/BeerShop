using Microsoft.VisualStudio.TestTools.UnitTesting;

using BeerShop.DataStore.Models;

namespace BeerShop.DataStore.Test
{
    [TestClass]
    public class BeerModelTest : EntityTest
    {
        [TestMethod]
        public void BeerNameRequired()
        {
            var beer = new Beer {Id = 1, Price = 1, Volume = 1, Country="UA" };
            Assert.IsTrue(ValidateModel(beer).Count > 0);
        }

        [TestMethod]
        public void BeerNameMaxLengthRequired()
        {
            var beer = new Beer { Id = 1, Name="123456789012345678901", Price = 1, Volume = 1, Country = "UA" };
            Assert.IsTrue(ValidateModel(beer).Count > 0);
        }

        [TestMethod]
        public void BeerVolumeRequired()
        {
            var beer = new Beer { Id = 1, Name="Beer1", Price = 1, Country = "UA" };
            Assert.IsTrue(ValidateModel(beer).Count > 0);
        }

        [TestMethod]
        public void BeerPriceRequired()
        {
            var beer = new Beer { Id = 1, Name = "Beer1", Volume = 1, Country = "UA" };
            Assert.IsTrue(ValidateModel(beer).Count > 0);
        }
        [TestMethod]
        public void BeerCountryNotRequired()
        {
            var beer = new Beer { Id = 1, Name = "Beer1", Volume = 1, Price = 1 };
            Assert.IsTrue(ValidateModel(beer).Count == 0);
        }

        [TestMethod]
        public void BeerCountryMaxLengthRequired()
        {
            var beer = new Beer { Id = 1, Name = "12345678901234567890", Price = 1, Volume = 1, Country = "123456789012345678901" };
            Assert.IsTrue(ValidateModel(beer).Count > 0);
        }
    }
}
