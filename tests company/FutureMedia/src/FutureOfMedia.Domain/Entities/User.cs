using FluentValidator.Validation;
using FutureOfMedia.Domain.ValueObjects;
using System;

namespace FutureOfMedia.Domain.Entities
{
    public class User : EntityBase
    {
        protected User() { } //because EF Core

        public User(Name name, string emailAddress, string phoneNumber)
        {
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            DeactivateEmail();
            DeactivatePhone(); //default is invisible

            AddNotifications(Name.Notifications);
            Validate();
        }

        public int UserId { get; private set; }
        public Name Name { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool EmailVisible { get; private set; }
        public bool PhoneVisible { get; private set; }
        public string ProfilePictureUrl { get; private set; }

        private void Validate()
        {
            AddNotifications(new ValidationContract()
                .IsNotNullOrEmpty(EmailAddress, "EmailAddress", "Should inform EmailAddress")
                .HasMaxLen(EmailAddress, 200, "EmailAddress", "EmailAddress caracters max 200")
                .IsNotNullOrEmpty(PhoneNumber, "PhoneNumber", "Should inform PhoneNumber")
                .HasMaxLen(PhoneNumber, 200, "PhoneNumber", "PhoneNumber caracters max 200")
            );
        }

        public void Update(Name name, string emailAddress, string phoneNumber, bool mailVisible, bool phoneVisible)
        {
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            if (mailVisible)
                ActivateEmail();
            else
                DeactivateEmail();
            if (phoneVisible)
                ActivatePhone();
            else
                DeactivatePhone();

            UpdatedIn = DateTime.Now;
            Validate();
        }

        public void UpdateProfileImage(string imagePath)
        {
            ProfilePictureUrl = imagePath;
        }

        public void ActivateEmail() => EmailVisible = true;
        public void DeactivateEmail() => EmailVisible = false;
        public void ActivatePhone() => PhoneVisible = true;
        public void DeactivatePhone() => PhoneVisible = false;

        public string getUserEmail(User user)
        {
            if (user.EmailVisible == true)
                return user.EmailAddress;
            return null;
        }

        public string getUserPhone(User user)
        {
            if (user.PhoneVisible == true)
                return user.PhoneNumber;
            return null; //exercise says to return 'null'
        }
    }
}