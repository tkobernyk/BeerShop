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
        [MaxLength(20)]
        public string Name { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public double Volume { get; set; }
        [MaxLength(20)]
        public string Country { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public double Price { get; set; }
        public ICollection<Brewery> Breweries { get; set; }
    }
}