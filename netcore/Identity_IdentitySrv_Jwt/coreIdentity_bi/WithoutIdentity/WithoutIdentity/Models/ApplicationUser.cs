using Microsoft.AspNetCore.Identity;
using System;

namespace WithoutIdentity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
