namespace BeerShop.DataStore.Infrastructure.Exception
{
    public class EntityNotFoundException : System.Exception
    {
        public EntityNotFoundException() : base("Entity not found")
        {}
    }
}
