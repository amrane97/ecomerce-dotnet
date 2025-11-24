
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Ordering.Application.Data;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        
        var totalCount = await dbContext.Orders.CountAsync();

        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ProjectToOrderDto())
            );
    }
}
