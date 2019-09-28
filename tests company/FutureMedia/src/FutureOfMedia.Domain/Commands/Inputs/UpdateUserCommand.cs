
namespace FutureOfMedia.Domain.Commands.Inputs
{
    public class UpdateUserCommand
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVisible { get; set; }
        public string Phone { get; set; }
        public bool PhoneVisible { get; set; }
    }
}