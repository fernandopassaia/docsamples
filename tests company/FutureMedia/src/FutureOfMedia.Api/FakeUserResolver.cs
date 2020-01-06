using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutureOfMedia.Api
{
    public static class FakeUserResolver
    {
        //On the Exercise, i should have a method to return my LoggedUser. And LoggedUser
        //should be a Static, will return all data (Email and Phone, even if they're hide)
        //and should be passed as a constant. So here it is the Constant.
        public static string loggedUser = "MySelf";
    }
}
