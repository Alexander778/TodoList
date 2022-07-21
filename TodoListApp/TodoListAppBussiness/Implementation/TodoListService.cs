using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoListAppBusiness.Interfaces;
using TodoListAppData.Models;
using TodoListAppData.Models.Enums;
using TodoListAppData.Models.Inputs;

namespace TodoListAppBusiness.Implementation
{
    public class TodoListService : ITodoListService
    {
        private readonly TodoListDbContext _dbContext;
        public TodoListService(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region User CRUD
        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }
        public async Task<int> AddUser(UserInput userInput)
        {
            User user = new User()
            {
                Name = userInput.Name,
                Surname = userInput.Surname
            };

            await _dbContext.Users.AddAsync(user);

            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Category CRUD

        public async Task<IEnumerable<Category>> GetCategoriesByUser(int userId, int? categoryId = null, int? todoTaskStatusId = null)
        {
            Expression<Func<Category, bool>> expression = i => i.UserId == userId;

            if (categoryId != null)
            {
                expression.And(i => i.Id == categoryId);
            }

            List<Category> userCategories = await _dbContext.Categories.Where(expression)
                .Include(i => i.TodoTasks)
                .ToListAsync();

            if (todoTaskStatusId != null)
            {
                foreach (var userCategory in userCategories)
                {
                    userCategory.TodoTasks = userCategory.TodoTasks.Where(i => i.StatusId == todoTaskStatusId).ToList();
                }
            }

            return userCategories;
        }
        public async Task<int> AddCategory(CategoryInput categoryInput)
        {
            Category category = new Category()
            {
                Name = categoryInput.Name,
                UserId = categoryInput.UserId
            };

            await _dbContext.Categories.AddAsync(category);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateCategory(int id, CategoryInput categoryInput)
        {
            var categoryToUpdate = _dbContext.Categories.FirstOrDefault(i => i.Id == id);
            if (categoryToUpdate == null)
            {
                throw new ArgumentNullException("Category was not found");
            }

            categoryToUpdate.Name = categoryInput.Name;

            _dbContext.Categories.Update(categoryToUpdate);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveCategory(int id)
        {
            var categoryToRemove = _dbContext.Categories.FirstOrDefault(i => i.Id == id);
            if (categoryToRemove == null)
            {
                throw new ArgumentNullException("Category was not found");
            }

            _dbContext.Categories.Remove(categoryToRemove);

            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region TodoTasks CRUD

        public async Task<int> AddTodoTask(TodoTaskInput todoTaskInput)
        {
            TodoTask todoTask = new TodoTask()
            {
                CategoryId = todoTaskInput.CategoryId,
                Text = todoTaskInput.Text,
                StatusId = (int)StatusEnum.Todo
            };

            await _dbContext.TodoTasks.AddAsync(todoTask);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateTodoTask(int id, TodoTaskInput todoTaskInput)
        {
            var todoTaskToUpdate = _dbContext.TodoTasks.FirstOrDefault(i => i.Id == id);
            if (todoTaskToUpdate == null)
            {
                throw new ArgumentNullException("TodoTask was not found");
            }

            todoTaskToUpdate.CategoryId = todoTaskInput.CategoryId;
            todoTaskToUpdate.Text = todoTaskInput.Text;
            todoTaskToUpdate.StatusId = todoTaskInput.StatusId;

            _dbContext.TodoTasks.Update(todoTaskToUpdate);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveTodoTask(int id)
        {
            var todoTaskToRemove = _dbContext.TodoTasks.FirstOrDefault(i => i.Id == id);
            if (todoTaskToRemove == null)
            {
                throw new ArgumentNullException("Todo task was not found");
            }

            _dbContext.TodoTasks.Remove(todoTaskToRemove);

            return await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
