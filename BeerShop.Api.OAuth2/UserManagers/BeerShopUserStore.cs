using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;
using BeerShop.Api.OAuth2.Models;

namespace BeerShop.Api.OAuth2.UserManagers
{
    public class BeerShopUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IClientCredentialsStore<ApplicationUser>
    {
        private static readonly IList<ApplicationUser> _users = new List<ApplicationUser>();
        private static readonly IDictionary<ApplicationUser, string> _passwords = new Dictionary<ApplicationUser, string>();
        
        public Task CreateAsync(ApplicationUser user)
        {
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            _users.Remove(user);
            return Task.FromResult(user);
        }

        public void Dispose()
        {}

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return Task.FromResult(user);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            var oldUser = FindByIdAsync(user.Id);
            _users.Remove(oldUser.Result);
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            _passwords.Add(user, passwordHash);
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(_passwords[user]);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(_passwords.ContainsKey(user));
        }

        public Task SetClientIdAndSecretAsync(ApplicationUser user, Guid clientId, string clientSecret)
        {
            if (_users.Contains(user))
            {
                _users.Remove(user);
            }
            user.ClientId = clientId;
            user.ClientSecret = clientSecret;
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<ApplicationUser> GetUserByClientIdAsync(Guid clientId)
        {
            var user = _users.FirstOrDefault(u => u.ClientId == clientId);
            return Task.FromResult(user);
        }

        public Task<ApplicationUser> GetUserByClientIdAndClientSecretAsync(Guid clientId, string clientSecret)
        {
            var user = _users.FirstOrDefault(u => u.ClientId == clientId && u.ClientSecret == clientSecret);
            return Task.FromResult(user);
        }

        public Task<bool> HasClientIdAndSecretAsync(ApplicationUser user)
        {
            var findedUser = _users.FirstOrDefault(u => u.Id == user.Id);
            return Task.FromResult(findedUser != null && 
                    findedUser.ClientId != Guid.Empty && 
                    !string.IsNullOrEmpty(findedUser.ClientSecret));
        }
    }
}