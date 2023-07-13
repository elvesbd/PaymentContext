using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void AddSubscription()
    {
        var name = new Name("Elves", "Brito");
        foreach (var not in name.Notifications)
        {

        }
    }
}