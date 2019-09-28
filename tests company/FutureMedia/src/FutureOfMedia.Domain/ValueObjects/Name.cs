using FluentValidator;
using FluentValidator.Validation;

namespace FutureOfMedia.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        protected Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new ValidationContract()
                .IsNotNullOrEmpty(FirstName, "FirstName", "Should inform FirstName")                
                .HasMaxLen(FirstName, 200, "FirstName", "FirstName caracters max 200")
                .IsNotNullOrEmpty(LastName, "LastName", "Should inform LastName")                
                .HasMaxLen(LastName, 200, "LastName", "LastName caracters max 200")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
