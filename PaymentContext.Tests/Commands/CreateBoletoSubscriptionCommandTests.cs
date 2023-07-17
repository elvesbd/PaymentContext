using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands;

[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "abcd";

        command.Validate();

        Assert.AreEqual(true, command.IsValid);
    }
}