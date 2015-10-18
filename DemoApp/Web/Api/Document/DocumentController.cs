using DemoApp.Data.Document;
using DemoApp.Data.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Dapper;

namespace DemoApp.Web.Api.Document
{
    [Authorize, RoutePrefix("api/document")]
    public class DocumentController : ApiController
    {
        [HttpPut, Route]
        public async Task Create(DocumentEntity request)
        {
            // This would create a connection, open it,
            // begin a transaction and put the repositories on
            // the uow.
            //using (var uow = new UnitOfWork(connectionString))
            //{
            //    var user = uow.Users.GetByUserName(userName);
            //}

            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
            {
                await connection.OpenAsync();

                //var transaction = connection.BeginTransaction();

                // Get the name of the logged in user from the controller.
                var userName = this.User.Identity.Name;

                // Get the user id of the logged in user from the database.
                var user = await new UserRepository(connection).GetByUserName(userName);
                var userId = user.UserId;

                // Set the created by to be the current user.
                request.CreatedBy = userId;
                
                // Set the created when to be now.
                request.CreatedWhen = DateTime.UtcNow;
                request.Title.ToString();
                // Create the new document in the database.
                await connection.ExecuteAsync(@"
INSERT INTO Document
(Title, Body, CreatedWhen, CreatedBy) 
VALUES 
(@Title, @Body, @CreatedWhen, @CreatedBy);
", request);
            }
        }

        [HttpGet, Route]
        public async Task<IEnumerable<DocumentEntity>> GetAll()
        {
            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
            {
                await connection.OpenAsync();

                // Read all the documents from the database.
                var result = await connection.QueryAsync<DocumentEntity>(@"
SELECT 
DocumentId,
Title, 
Body,
CreatedWhen,
CreatedBy
FROM 
Document;
");

                return result;
            }
        }
    }
}
