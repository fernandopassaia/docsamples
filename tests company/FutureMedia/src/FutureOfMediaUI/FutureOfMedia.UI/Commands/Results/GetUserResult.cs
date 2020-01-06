using Newtonsoft.Json;

namespace FutureOfMedia.UI.Commands.Results
{
    [JsonObject] //if i don't decorated my class with this attribute, Deserialization don't works. Why lord?
    public class GetUserResult
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
