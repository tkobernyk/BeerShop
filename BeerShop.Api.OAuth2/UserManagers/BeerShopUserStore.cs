using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;
using BeerShop.Api.OAuth2.Models;

namespace BeerShop.Api.OAuth2.UserManagers
{
    public class BeerShopUserStore : IUserStore<ApplicationUser>
    {
        private static readonly IList<ApplicationUser> _users;
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
        {
            return;
        }

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
    }
}