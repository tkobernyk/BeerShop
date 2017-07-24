using System.Linq;
using BeerShop.DataStore.Models;

namespace BeerShop.Api.Tests.Stubs
{
    class FakeBeerDbSet : FakeDbSet<Beer>
    {
        public override Beer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.Id == (int)keyValues.Single());
        }
        public override Beer FindByName(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.Name == (string)keyValues.Single());
        }
    }
}
