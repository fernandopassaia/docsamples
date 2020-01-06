using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Commands
{
    public class CreateUserCommand
    {
        //Note: This is CQRS and in a Pratical Project, create a Package and Separate this Stuff Good, ok?
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } //here I will pass the Role this is not the best implementation, it's just for TEST
        //purpose and show how Roles works on WebApi IdentityServer and how to implement it on API consumed by Angular UI.
    }
}
