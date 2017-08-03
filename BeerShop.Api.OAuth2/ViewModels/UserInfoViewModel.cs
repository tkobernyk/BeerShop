using System;
namespace BeerShop.Api.OAuth2.ViewModels
{
    public class UserInfoViewModel
    {
        public string Login { get; set; }
        public bool HasRegistered { get; set; }
        public string LoginProvider { get; set; }
    }
}