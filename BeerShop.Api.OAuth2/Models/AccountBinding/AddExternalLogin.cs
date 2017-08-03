using System.ComponentModel.DataAnnotations;

namespace BeerShop.Api.OAuth2.Models.AccountBinding
{
    public class AddExternalLogin
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}