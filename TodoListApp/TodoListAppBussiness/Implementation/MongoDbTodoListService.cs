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
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, userId) &
                filterBuilder.ElemMatch(doc => doc.Categories, el => el.Id == categoryId);

            var updateBuilder = Builders<User>.Update;
            var update = updateBuilder.Set(doc => doc.Categories[-1].Name, categoryInput.Name);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task AddTodoTask(string userId, string categoryId, TodoTask todoTaskInput)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, userId) &
                filterBuilder.ElemMatch(doc => doc.Categories, el => el.Id == categoryId);

            var updateBuilder = Builders<User>.Update;

            todoTaskInput.Id = ObjectId.GenerateNewId().ToString();
            var update = updateBuilder.AddToSet(doc => doc.Categories[-1].TodoTasks, todoTaskInput);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTodoTask(string userId, string categoryId, TodoTask todoTaskInput)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, userId) &
                filterBuilder.ElemMatch(doc => doc.Categories, el => el.Id == categoryId) &
                filterBuilder.ElemMatch(doc => doc.Categories[-1].TodoTasks, el=> el.Id == todoTaskInput.Id);

            var updateBuilder = Builders<User>.Update;
            var update = updateBuilder.Set(doc => doc.Categories[-1].TodoTasks[-1], todoTaskInput);

            await _userCollection.FindOneAndUpdateAsync(filter, update);
        }
    }
}
