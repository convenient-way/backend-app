using unitofwork_core.Model.Collection;

namespace unitofwork_core.Model.ApiResponse
{
    public class ApiResponsePaginated<T> : ApiResponse<List<T>>
    {
        public Paginated? Paginated { get; }

        public ApiResponsePaginated() { }
        public ApiResponsePaginated(PaginatedList<T> items) {
            Paginated = new Paginated();
            Paginated.PageIndex = items.PageIndex;
            Paginated.PageSize = items.PageSize;
            Paginated.TotalCount = items.TotalCount;
            Paginated.TotalPage = items.TotalPage;
            Data = items;
        }
    }

    public class Paginated {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
    }
}
