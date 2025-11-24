namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>
    (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public long Count { get; }
    public IEnumerable<TEntity> Data { get; }
}
