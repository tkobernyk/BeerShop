
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Api.OAuth2.Models.AccountBinding
{
    public class RemoveLogin
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }
}