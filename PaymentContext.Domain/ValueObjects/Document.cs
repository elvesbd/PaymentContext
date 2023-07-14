using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType type)
    {
        Number = number;
        Type = type;

        AddNotifications(new Contract<Document>()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Invalid document")
        );
    }

    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }
    private bool Validate()
    {
        const int CNPJLength = 14;
        const int CPFLength = 11;
        if (Type == EDocumentType.CNPJ && Number.Length == CNPJLength)
            return true;

        if (Type == EDocumentType.CPF && Number.Length == CPFLength)
            return true;

        return false;
    }
}