using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DemoApp.Data.User
{
    public class UserRepository
    {
        private MySqlConnection _openConnection;

        public UserRepository(MySqlConnection openConnection)
        {
            this._openConnection = openConnection;
        }

        public async Task<UserEntity> GetByUserName(string userName)
        {
            // Read the user by their username in the database.
            var results = await this._openConnection.QueryAsync<UserEntity>(@"
SELECT 
UserId,
UserName,
PasswordHash
FROM 
user
WHERE UserName = @UserName
", new { UserName = userName });

            var result = results.FirstOrDefault();
            return result;
        }
    }
}
