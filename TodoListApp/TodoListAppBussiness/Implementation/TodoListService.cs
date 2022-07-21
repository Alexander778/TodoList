using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAppBusiness.Interfaces;
using TodoListAppData.Models;
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
    }
}
