namespace unitofwork_core.Model.Collection
{
    public static class IEnumerablePagedListExtension
    {
        public static PaginatedList<T> ToPagedList<T>(this IEnumerable<T> source,int totalCount, int pageIndex, int pageSize) => new PaginatedList<T>(source.ToList(),totalCount, pageIndex, pageSize);
    }
}
