using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAppData.Models.Inputs;

namespace TodoListAppBusiness.Interfaces
{
    public interface ITodoListService
    {
        Task<int> AddUser(UserInput userInput);
    }
}
