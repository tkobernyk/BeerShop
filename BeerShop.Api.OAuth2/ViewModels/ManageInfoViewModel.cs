using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerShop.Api.OAuth2.ViewModels
{
    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Login { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }
}