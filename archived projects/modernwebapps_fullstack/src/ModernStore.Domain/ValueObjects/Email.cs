using FluentValidator;
using FluentValidator.Validation;

namespace ModernStore.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email() { }
        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;

            AddNotifications(new ValidationContract()
                //.IsEmail("EmailAddress", "EmailAddress", "Should inform EmailAddress in Correct Format")
                .HasMaxLen(EmailAddress, 60, "EmailAddress", "EmailAddress should be max 60")
            );            
        }

        public string EmailAddress { get; private set; }
    }
}
