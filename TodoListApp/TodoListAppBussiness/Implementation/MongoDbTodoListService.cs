using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoListAppBusiness.Interfaces;
using TodoListAppData;
using TodoListAppData.MongoDbModels;

namespace TodoListAppBusiness.Implementation
{
    public class MongoTodoListService : IMongoTodoListService
    {
        private readonly IMongoCollection<User> _userCollection;

        public MongoTodoListService(IOptions<TodoListDatabaseSettings> todoListDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            todoListDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                todoListDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                todoListDatabaseSettings.Value.TodoListCollectionName);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }

        public async Task AddUser(User userInput)
        {
            await _userCollection.InsertOneAsync(userInput);
        }

        public async Task UpdateUser(string id, User userInput)
        {
            userInput.Id = id;

            await _userCollection.ReplaceOneAsync(x => x.Id == id, userInput);
        }

        //public async Task<List<User>> GetCategories()
        //{
        //    return await _userCollection.Find(_ => true).ToListAsync();
        //}

        public async Task AddCategory(string userId, Category categoryInput)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq("Id", userId));

            categoryInput.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<User>.Update.Push("Categories", categoryInput);

            await _userCollection.FindOneAndUpdateAsync<User>(filter, update);
        }

        public async Task UpdateCategory(string userId, string categoryId, Category categoryInput)
        {
            //var filter = Builders<User>.Filter.And(
            //    Builders<User>.Filter.Eq("Id", userId),
            //    Builders<User>.Filter.Eq("Id", categoryId));

            // var filter1 = Builders<User>.Filter.Eq("Id", userId);
            var filter2 = Builders<Category>.Filter.Eq("Id", categoryId);
            // var filterAnd = Builders<User>.Filter.And(filter1);
        }
    }
}
