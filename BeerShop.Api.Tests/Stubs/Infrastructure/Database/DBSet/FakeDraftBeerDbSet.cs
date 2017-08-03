using System.Linq;
using BeerShop.DataStore.Models.v2;

namespace BeerShop.Api.Tests.Stubs.Infrastructure.Database.DBSet
{
    class FakeDraftBeerDbSet : FakeDbSetBase<DraftBeer>
    {
        public override DraftBeer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.Id == (int)keyValues.Single());
        }
    }
}
