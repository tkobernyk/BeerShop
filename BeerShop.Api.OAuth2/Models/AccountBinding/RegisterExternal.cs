using System.ComponentModel.DataAnnotations;

namespace BeerShop.Api.OAuth2.Models.AccountBinding
{
    public class RegisterExternal
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}