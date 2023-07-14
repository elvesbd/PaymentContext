using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Name>()
            .Requires()
            .IsGreaterThan(FirstName, 3, "Name.FirstName", "FirstName must contain at greater than 3 characters")
            .IsLowerThan(FirstName, 40, "Name.FirstName", "FirstName must contain a lower than 40 characters")
            .IsGreaterThan(LastName, 3, "Name.LastName", "LastName must contain at greater than 3 characters")
        );
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}