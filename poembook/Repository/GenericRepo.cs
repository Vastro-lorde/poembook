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
                var updateDefinition = new List<UpdateDefinition<T>>();

                // Reflect through the properties of the entity to build update definition only for non-null properties
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(entity);
                    if (value != null)
                    {
                        // Only include properties that are not null
                        var field = property.Name;

                        // checking if the field is Id
                        if (field != "Id")
                        {
                            updateDefinition.Add(Builders<T>.Update.Set(field, value));
                        }
                        
                    }
                }

                if (updateDefinition.Count > 0)
                {
                    var combinedUpdate = Builders<T>.Update.Combine(updateDefinition);
                    var result = await _collection.UpdateOneAsync(e => e.Id == id, combinedUpdate);

                    if (result.MatchedCount == 0)
                    {
                        throw new Exception($"Entity with id {id} not found");
                    }

                    if (result.ModifiedCount == 0)
                    {
                        throw new Exception($"No properties were updated for entity with id {id}");
                    }

                    // Get the updated entity
                    entity = await GetByIdAsync(id);
                }

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
