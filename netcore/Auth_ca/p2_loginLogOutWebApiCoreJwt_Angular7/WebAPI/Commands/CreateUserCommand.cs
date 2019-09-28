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
    }
}
