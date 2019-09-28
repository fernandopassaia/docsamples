using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityChangeNames.Models
{
    //4 alteração: Criei uma classe ApplicationRole pra também modificar pra Guid ao invés de string
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
