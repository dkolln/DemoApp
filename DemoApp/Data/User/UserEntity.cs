using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data.User
{
    public class UserEntity : IUser<string>
    {
        public UserEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id
        {
            get;
            private set;
        }
        public int UserId { get; set; }

        public string PasswordHash { get; set; }
        public string UserName { get; set; }
    }
}
