using MongoDB.Driver;
using poembook.Models;
using poembook.Services.Pagination;

namespace poembook.Services
{
    public interface IPoemService
    {
        Task<PoemModel> CreatePoem(PoemModel poem);
        Task<DeleteResult> DeletePoem(string id);
        Task<PoemModel> EditPoem(string id, PoemModel poem);
        Task<PaginationResponse<PoemModel>> GetAllPoems(int pageNumber, int pageSize);
        Task<PoemModel> GetPoemById(string id);
    }
}