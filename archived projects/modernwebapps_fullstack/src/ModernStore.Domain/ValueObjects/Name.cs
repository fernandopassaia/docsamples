using FluentValidator;
using FluentValidator.Validation;

namespace ModernStore.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        protected Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new ValidationContract()
                .IsNotNull(FirstName, "FirstName", "Should inform FirstName")
                .HasMinLen(FirstName, 3, "FirstName", "FirstName caracters min 3")
                .HasMaxLen(FirstName, 60, "FirstName", "FirstName caracters max 60")
                .IsNotNull(LastName, "LastName", "Should inform LastName")
                .HasMinLen(LastName, 3, "LastName", "LastName caracters min 3")
                .HasMaxLen(LastName, 60, "LastName", "LastName caracters max 60")
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
