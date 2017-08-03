using Microsoft.VisualStudio.TestTools.UnitTesting;

using BeerShop.DataStore.Models.v2;

namespace BeerShop.DataStore.Test
{
    [TestClass]
    public class DraftBeerModelTest : EntityTest
    {
        [TestMethod]
        [TestCategory("DataStore.Models.Validation")]
        public void DraftBeerIsDraftNotRequired()
        {
            var beer = new DraftBeer { Id = 1, Name = "Beer1", Volume = 1, Price = 1 };
            Assert.IsTrue(ValidateModel(beer).Count == 0);
        }
    }
}
