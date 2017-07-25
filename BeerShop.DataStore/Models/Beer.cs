using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeerShop.DataStore.Models.Interfaces;

namespace BeerShop.DataStore.Models
{
    public class Beer : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Volume { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public ICollection<Brewery> Breweries { get; set; }
    }
}