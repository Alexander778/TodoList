using TodoListAppData.MongoDbModels;

namespace TodoListAppBusiness.Interfaces
{
    public interface IMongoTodoListService
    {
        Task<List<User>> GetUsers();
        Task AddUser(User userInput);
        Task UpdateUser(string id, User userInput);

        Task AddCategory(string userId, Category categoryInput);
        Task UpdateCategory(string userId, string name, Category categoryInput);
    }
}
