using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAppData.Models;
using TodoListAppData.Models.Inputs;

namespace TodoListAppBusiness.Interfaces
{
    public interface ITodoListService
    {
        IEnumerable<User> GetUsers();
        Task<int> AddUser(UserInput userInput);
        Task<int> UpdateUser(UserInput userInput);

        Task<IEnumerable<Category>> GetCategoriesByUser(int userId, int? categoryId = null, int? todoTaskStatusId = null);
        Task<int> AddCategory(CategoryInput categoryInput);
        Task<int> UpdateCategory(CategoryInput categoryInput);
        Task<int> RemoveCategory(int id);

        Task<int> AddTodoTask(TodoTaskInput todoTaskInput);
        Task<int> UpdateTodoTask(TodoTaskInput todoTaskInput);
        Task<int> RemoveTodoTask(int id);
    }
}
