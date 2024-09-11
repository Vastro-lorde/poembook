using Amazon.Runtime;
using MongoDB.Driver;
using poembook.Data;
using poembook.Models;
using poembook.Services.Pagination;

namespace poembook.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private IMongoCollection<T> _collection;

        public GenericRepo(IMongoDbContext context, string collectionName)
        {
            _collection = context._database.GetCollection<T>(collectionName);
        }

        public async Task<PaginationResponse<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            var data = await _collection.Find(_ => true).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();

            int totalItems = (int)await _collection.CountDocumentsAsync(_ => true);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);


            return new PaginationResponse<T>(pageNumber, pageSize, totalPages, totalItems, data );
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.FindAsync(entity => entity.Id == id).Result.FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        public async Task<T> UpdateAsync(string id, T entity)
        {
            try
            {
                await _collection.ReplaceOneAsync(entity => entity.Id == id, entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(entity => entity.Id == id);

                if (result.DeletedCount == 0)
                {
                    throw new Exception($"Entity with id {id} not found");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
