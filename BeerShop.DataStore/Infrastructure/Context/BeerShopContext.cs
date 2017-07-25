using System.Data.Entity;
using BeerShop.DataStore.Models;

namespace BeerShop.DataStore.Infrastructure.Context
{
    public class BeerShopContext : DbContext, IBeerShopContext
    {
        public BeerShopContext(): base()
        {}

        public IDbSet<Brewery> Breweries { get; set; }
        public IDbSet<Beer> Beers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .HasMany(b => b.Breweries)
                .WithMany(br => br.Beers)
                .Map(cs =>
                {
                    cs.MapLeftKey("BeerRefId");
                    cs.MapRightKey("BreweryRefId");
                    cs.ToTable("BeerBrewery");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
