using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class NotFoundOrderException : NotFoundException
{
    public NotFoundOrderException(Guid id) : base("Order", id)
    {
    }

}
