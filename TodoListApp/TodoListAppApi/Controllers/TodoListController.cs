using Microsoft.AspNetCore.Mvc;
using TodoListAppBusiness.Interfaces;
using TodoListAppData.Models.Inputs;

namespace TodoListAppApi.Controllers
{
    [ApiController]
    [Route("/todolist")]
    public class TodoListController: ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        #region User

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            var users = _todoListService.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUser([FromBody] UserInput userInput) 
        {
            var userCreation = await _todoListService.AddUser(userInput);

            return Ok(userCreation);
        }

        [HttpPost]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInput userInput)
        {
            var userCreation = await _todoListService.UpdateUser(userInput);

            return Ok(userCreation);
        }

        #endregion

        #region Category

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategoriesByUser(int userId, int? categoryId = null, int? todoTaskStatusId = null)
        {
            var categories = await _todoListService.GetCategoriesByUser(userId, categoryId, todoTaskStatusId);

            return Ok(categories);
        }

        [HttpPost]
        [Route("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryInput categoryInput)
        {
            var categoryCreation = await _todoListService.AddCategory(categoryInput);

            return Ok(categoryCreation);
        }

        [HttpPost]
        [Route("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryInput categoryInput)
        {
            var categoryCreation = await _todoListService.UpdateCategory(categoryInput);

            return Ok(categoryCreation);
        }

        [HttpPost]
        [Route("remove-category/{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var categoryCreation = await _todoListService.RemoveCategory(id);

            return Ok(categoryCreation);
        }

        #endregion

        #region TodoTask

        [HttpPost]
        [Route("add-task")]
        public async Task<IActionResult> AddTodoTask([FromBody] TodoTaskInput todoTaskInput)
        {
            var categoryCreation = await _todoListService.AddTodoTask(todoTaskInput);

            return Ok(categoryCreation);
        }

        [HttpPost]
        [Route("update-task")]
        public async Task<IActionResult> UpdateTodoTask([FromBody] TodoTaskInput todoTaskInput)
        {
            var categoryCreation = await _todoListService.UpdateTodoTask(todoTaskInput);

            return Ok(categoryCreation);
        }

        [HttpPost]
        [Route("remove-task/{id}")]
        public async Task<IActionResult> RemoveTodoTask(int id)
        {
            var categoryCreation = await _todoListService.RemoveTodoTask(id);

            return Ok(categoryCreation);
        }

        #endregion
    }
}
