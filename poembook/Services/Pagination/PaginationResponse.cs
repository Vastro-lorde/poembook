namespace poembook.Services.Pagination
{
    public class PaginationResponse<T>(int pageNumber, int pageSize, int totalPages, int totalRecords, IEnumerable<T> data)
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalPages { get; set; } = totalPages;
        public int TotalRecords { get; set; } = totalRecords;
        public IEnumerable<T> Data { get; set; } = data;
    }
}
