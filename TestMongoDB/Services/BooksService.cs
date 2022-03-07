using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMongoDB.Model;

namespace TestMongoDB.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BooksService(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Book>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task CreateManyAsync(List<Book> newBooks) =>
           await _booksCollection.InsertManyAsync(newBooks);

        public async Task UpdateAsync(string id, Book updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id.ToString() == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id.ToString() == id);
    }
}
