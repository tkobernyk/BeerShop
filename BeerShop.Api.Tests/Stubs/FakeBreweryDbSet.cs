using System.Linq;
using BeerShop.DataStore.Models;

namespace BeerShop.Api.Tests.Stubs
{
    class FakeBreweryDbSet : FakeDbSet<Brewery>
    {
        public override Brewery Find(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.Id == (int)keyValues.Single());
        }
    }
}
