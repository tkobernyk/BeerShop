namespace BeerShop.DataStore.Models.v2
{
    public class DraftBeer : Beer
    {
        public bool IsDraft { get; set; }

        public static Beer ToBeer(DraftBeer draftBeer)
        {
            return draftBeer;
        }

        public static DraftBeer FromBeer(Beer beer)
        {
            return new DraftBeer
            {
                Id = beer.Id,
                Name = beer.Name,
                Volume = beer.Volume,
                Price = beer.Price,
                Country = beer.Country,
                Breweries = beer.Breweries
            };
        }
    }
}
