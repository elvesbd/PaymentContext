using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;

        AddNotifications(new Contract<Address>()
            .Requires()
            .IsGreaterThan(Street, 5, "Address.Street", "Street must contain at greater than 3 characters")
            .IsGreaterThan(Number, 1, "Address.Number", "Number must contain at greater than 3 characters")
            .IsLowerThan(Number, 10, "Address.Number", "Number must contain a lower than 40 characters")
            .IsGreaterThan(Neighborhood, 3, "Address.Neighborhood", "Neighborhood must contain at greater than 3 characters")
            .IsGreaterThan(City, 3, "Address.City", "City must contain at greater than 3 characters")
            .IsGreaterOrEqualsThan(State, 2, "Address.State", "State must contain at greater than 2 characters")
            .IsLowerOrEqualsThan(State, 2, "Address.State", "State must contain at greater than 2 characters")
            .IsGreaterThan(Country, 3, "Address.Country", "Country must contain at greater than 3 characters")
            .IsGreaterOrEqualsThan(ZipCode, 3, "Address.ZipCode", "ZipCode must contain at greater than 3 characters")
            .IsLowerOrEqualsThan(ZipCode, 8, "Address.ZipCode", "ZipCode must contain at greater than 8 characters")
        );
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
}