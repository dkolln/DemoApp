using DemoApp.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DemoApp.Data.User;

namespace DemoApp.Web.Security
{
    public class DemoAppUserStore : IUserStore<UserEntity>, IUserPasswordStore<UserEntity>
    {
        public async Task CreateAsync(UserEntity user)
        {
            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync(@"
INSERT INTO User 
(UserName, PasswordHash) 
VALUES 
(@UserName, @PasswordHash);
", user);
            }
        }

        public Task UpdateAsync(UserEntity user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserEntity user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<UserEntity> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> FindByNameAsync(string userName)
        {
            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
            {
                await connection.OpenAsync();

                var result = await new UserRepository(connection).GetByUserName(userName);

                return result;
            }
        }

        public Task SetPasswordHashAsync(UserEntity user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(UserEntity user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserEntity user)
        {
            var result = user.PasswordHash != null;
            return Task.FromResult(result);
        }

        public void Dispose()
        {
            // There's nothing to dispose of at the moment.
        }
    }
}
