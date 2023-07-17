using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    private readonly Student _student;
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Elves", "Brito");
        _document = new Document("99866534211", EDocumentType.CPF);
        _email = new Email("elves@tests.com");
        _address = new Address(
            "Travessa Maciel",
            "100",
            "Montese",
            "Fortaleza",
            "CE",
            "Brasil",
            "60000000"
        );
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment(
            "12345678",
            DateTime.Now,
            DateTime.Now.AddDays(5),
            10,
            10,
            _document,
            "WAYNE CORP",
            _address,
            _email
        );
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        var payment = new PayPalPayment(
           "12345678",
           DateTime.Now,
           DateTime.Now.AddDays(5),
           10,
           10,
           _document,
           "WAYNE CORP",
           _address,
           _email
       );
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.IsValid);
    }
}