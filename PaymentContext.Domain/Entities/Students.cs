using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    private readonly IList<Subscription> _subscriptions;
    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address? Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }
    public void AddSubscription(Subscription subscription)
    {
        var hasSubscriptionActive = false;
        foreach (var sub in _subscriptions)
        {
            if (sub.Active)
                hasSubscriptionActive = true;
        }

        AddNotifications(new Contract<Subscription>()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "you already have an active subscription")
            .AreNotEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "This subscription has no payment")
        );

        if (IsValid)
            _subscriptions.Add(subscription);

        /* if (hasSubscriptionActive)
            AddNotification("Student.Subscriptions", "you already have an active subscription"); */
    }
}