using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to complete your subscription");
        }

        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "This CPF is already in use");

        if (_repository.EmailExistis(command.Email))
            AddNotification("Document", "This E-MAIL is already in use");

        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.Country,
            command.ZipCode
        );

        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BarCode,
            command.BoletoNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            new Document(command.Document, command.PayerDocumentType),
            command.Payer,
            address,
            email
        );

        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        AddNotifications(name, document, address, student, subscription, payment);

        if (!IsValid)
            return new CommandResult(false, "Unable to create your signature");

        _repository.CreateSubscription(student);
        _emailService.Send(
            student.Name.ToString(),
            student.Email.Address,
            "Welcome",
            "Your signature has been created"
        );

        return new CommandResult(true, "Successful subscription");
    }
}
