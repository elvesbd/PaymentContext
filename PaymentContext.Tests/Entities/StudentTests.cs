using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void AddSubscription()
    {
        var subscription = new Subscription(DateTime.Now);
        var student = new Student("Elves", "Damasceno", "12345678912", "elves@mail.com");
        student.AddSubscription(subscription);
    }
}