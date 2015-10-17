using DemoApp.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using DemoApp.Data.User;
using System.Configuration;
using Dapper;
using MySql.Data.MySqlClient;

namespace DemoApp.Web.Api.User
{
    [Authorize, RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [AllowAnonymous, HttpGet, Route("{userName}/password/{password}")]
        public async Task<IdentityResult> Create(string userName, string password)
        {
            var user = new UserEntity()
            {
                UserName = userName
            };

            var userManager = new DemoAppUserManager(new DemoAppUserStore());
            var result = await userManager.CreateAsync(user, password);

            return result;
        }

        [HttpGet, Route]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
            {
                await connection.OpenAsync();

                // Read all the userId's and userName's from the database, but don't send their password hashes.
                var result = await connection.QueryAsync<UserDto>(@"
SELECT 
UserId,
UserName
FROM 
User
");
                return result;
            }
        }
    }
}
