using FluentValidator;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Resources;
using ModernStore.Domain.Services;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class CustomerCommandHandler : Notifiable,
        ICommandHandler<RegisterCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }        

        public ICommandResult Handle(RegisterCustomerCommand command)
        {
            // Step 1: Check if Document Already Exists
            if (_customerRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "This Document Already in Use!");
                return null;
            }

            // Step 2: Create the Value Objects
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);
            var user = new User(command.Username, command.Password, command.ConfirmPassword);
            var customer = new Customer(name, email, document, user);

            // Step 3: If there's some notification (validation) i will insert on this object
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(user.Notifications);
            AddNotifications(customer.Notifications);
            
            if (!Valid) //if there's notification, i will stop
                return null;

            // Step 4: Save and Register the Customer
            _customerRepository.Save(customer);

            // Step 5: Send email with Welcome Message
            _emailService.Send(
                customer.Name.ToString(), 
                customer.Email.EmailAddress, 
                string.Format(EmailTemplates.WelcomeEmailTitle, customer.Name),
                string.Format(EmailTemplates.WelcomeEmailBody, customer.Name));

            // Step 6: Return the Result
            return new RegisterCustomerCommandResult(customer.CustomerId, customer.Name.ToString());
        }
    }
}
