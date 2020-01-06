using System;
using System.Collections.Generic;
using System.Text;

namespace FutureOfMedia.Domain.Commands.Results
{
    public class GetUserDetailResult
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
