using DemoApp.Data.User;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Web.Security
{
    public class DemoAppUserManager : UserManager<UserEntity>
    {
        public DemoAppUserManager(DemoAppUserStore store) :
            base(store)
        {
            // TODO: Make this configurable and/or change the values.
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
        }
    }
}
