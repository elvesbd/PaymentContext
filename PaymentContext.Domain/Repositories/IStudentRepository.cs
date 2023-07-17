using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories;

public interface IStudentRepository
{
    bool DocumentExists(string document);
    bool EmailExistis(string email);
    void CreateSubscription(Student student);
}