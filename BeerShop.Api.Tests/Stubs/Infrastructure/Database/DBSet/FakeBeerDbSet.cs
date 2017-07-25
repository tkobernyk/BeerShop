using System.Linq;
using BeerShop.DataStore.Models;

namespace BeerShop.Api.Tests.Stubs.Infrastructure.Database.DBSet
{
    class FakeBeerDbSet : FakeDbSetBase<Beer>
    {
        public override Beer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.Id == (int)keyValues.Single());
        }
    }
}
