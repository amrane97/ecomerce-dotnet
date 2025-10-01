namespace Ordering.Domain.ValueObject;

public record OrderName
{
    public string Value { get; }
    private OrderName(string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value is null)
        {
            throw new DomainException("OrderItemId cannot be empty");
        }

        return new OrderName(value);
    }
}
