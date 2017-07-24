using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeerShop.DataStore.Models.Interfaces;

namespace BeerShop.DataStore.Models
{
    public class Brewery : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Beer> Beers { get; set; }
    }
}
