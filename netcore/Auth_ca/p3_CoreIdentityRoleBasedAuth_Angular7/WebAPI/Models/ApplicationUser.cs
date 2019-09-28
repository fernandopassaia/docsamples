using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        //here i receive my IdentityUser and customize it to fields i want too
        [Column(TypeName ="nvarchar(150)")] //pass it to MAP in the Future
        public string FullName { get; set; }
    }
}
