using System;
using System.Collections.Generic;
using System.Text;

namespace FutureOfMedia.Domain.Commands.Results
{
    public class GetLoggedUserDetailResult
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailVisible { get; set; }
        public bool PhoneVisible { get; set; }
    }
}
