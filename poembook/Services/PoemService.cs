using MongoDB.Driver;
using poembook.Models;
using poembook.Repository;
using poembook.Services.Pagination;

namespace poembook.Services
{
    public class PoemService : IPoemService
    {
        private readonly IGenericRepo<PoemModel> _poemRepo; // <Poem>
        public PoemService(IGenericRepo<PoemModel> poemRepo)
        {
            _poemRepo = poemRepo;
        }

        /// <summary>
        /// Gets all the poems in the database
        /// </summary>
        /// <returns>An enumerable of poem models</returns>
        public async Task<PaginationResponse<PoemModel>> GetAllPoems(int pageNumber, int pageSize)
        {
            return await _poemRepo.GetAllAsync(pageNumber, pageSize);
        }

        /// <summary>
        /// Gets a poem by ID
        /// </summary>
        /// <param name="id">The ID of the poem to get</param>
        /// <returns>The poem model that matches the ID</returns>
        public async Task<PoemModel> GetPoemById(string id)
        {
            return await _poemRepo.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a poem in the database
        /// </summary>
        /// <param name="poem">The poem model to create</param>
        /// <returns>The created poem model</returns>
        public async Task<PoemModel> CreatePoem(PoemModel poem)
        {
            try
            {
                var result = await _poemRepo.CreateAsync(poem);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Edits a poem by ID
        /// </summary>
        /// <param name="id">The ID of the poem to edit</param>
        /// <param name="poem">The poem model to update</param>
        /// <returns>The updated poem model</returns>
        public async Task<PoemModel> EditPoem(string id, PoemModel poem)
        {
            try
            {
                var result = await _poemRepo.UpdateAsync(id, poem);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a poem by ID
        /// </summary>
        /// <param name="id">The ID of the poem to delete</param>
        /// <returns>The result of the deletion operation</returns>
        public async Task<DeleteResult> DeletePoem(string id)
        {
            try
            {
                var result = await _poemRepo.DeleteAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
