using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var fakeStudentRepository = new FakeStudentRepository();
        var fakeEmailService = new FakeEmailService();
        var handler = new SubscriptionHandler(fakeStudentRepository, fakeEmailService);
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Bruce";
        command.LastName = "Wayne";
        command.Document = "99999999999";
        command.Email = "hello@balta.io2";
        command.BarCode = "123456789";
        command.BoletoNumber = "1234654987";
        command.PaymentNumber = "123121";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.Payer = "WAYNE CORP";
        command.PayerDocument = "12345678911";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail = "batman@dc.com";
        command.Street = "Rua Santa Eliza";
        command.Number = "224";
        command.Neighborhood = "Pirambu";
        command.City = "Fortaleza";
        command.State = "CE";
        command.Country = "Brasil";
        command.ZipCode = "60000000";

        handler.Handle(command);
        Assert.AreEqual(false, handler.IsValid);
    }
}