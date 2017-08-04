using System;
using System.Threading.Tasks;

namespace BeerShop.Api.OAuth2.UserManagers
{
    public interface IClientCredentialsStore<TUser>
    {
        Task SetClientIdAndSecretAsync(TUser user, Guid clientId, string clientSecret);

        Task<TUser> GetUserByClientIdAsync(Guid clientId);

        Task<TUser> GetUserByClientIdAndClientSecretAsync(Guid clientId, string clientSecret);

        Task<bool> HasClientIdAndSecretAsync(TUser user);
    }
}