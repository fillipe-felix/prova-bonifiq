namespace ProvaPub.Services;

public abstract class PagedListService
{
    protected const int PageSize = 10;

    protected PagedListService()
    {
    }
    
    protected List<T> GetPagedData<T>(IQueryable<T> query, int page, out int totalCount, out bool hasNext)
    {
        page = Math.Max(1, page);
        int skip = (page - 1) * PageSize;
        totalCount = query.Count();

        var items = query
            .Skip(skip)
            .Take(PageSize)
            .ToList();
            

        hasNext = (skip + PageSize) < totalCount;
            
        return items;
    }
}