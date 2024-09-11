using MongoDB.Driver;
using poembook.Models;
using poembook.Services.Pagination;

namespace poembook.Repository
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<DeleteResult> DeleteAsync(string id);
        Task<PaginationResponse<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(string id);
        Task<T> UpdateAsync(string id, T entity);
    }
}