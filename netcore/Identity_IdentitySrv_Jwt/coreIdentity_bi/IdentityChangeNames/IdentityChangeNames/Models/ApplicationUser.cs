using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityChangeNames.Models
{
    //public class ApplicationUser : IdentityUser
    //3 alteração: Entrei na classe ApplicationUser e mudei meu IdentityUser pra Guid ao invés de string
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
