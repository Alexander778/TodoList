using Microsoft.AspNetCore.Mvc;
using TodoListAppBusiness.Interfaces;
using TodoListAppData.Models.Inputs;
using TodoListAppData.MongoDbModels;

namespace TodoListAppApi.Controllers
{
    [ApiController]
    [Route("/mongo/todolist")]
    public class TodoListMongoDbController : ControllerBase
    {
        private readonly IMongoTodoListService _todoListService;

        public TodoListMongoDbController(IMongoTodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        #region User

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _todoListService.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUser([FromBody] User userInput)
        {
            await _todoListService.AddUser(userInput);

            return Ok();
        }

        [HttpPost]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User userInput)
        {
            await _todoListService.UpdateUser(id, userInput);

            return Ok();
        }

        #endregion

        //#region Category

        //[HttpGet]
        //[Route("categories")]
        //public async Task<IActionResult> GetCategoriesByUser(int userId, int? categoryId = null, int? todoTaskStatusId = null)
        //{
        //    var categories = await _todoListService.GetCategoriesByUser(userId, categoryId, todoTaskStatusId);

        //    return Ok(categories);
        //}

        [HttpPost]
        [Route("add-category")]
        public async Task<IActionResult> AddCategory(string userId, [FromBody] Category categoryInput)
        {
            await _todoListService.AddCategory(userId, categoryInput);

            return Ok();
        }

        [HttpPost]
        [Route("update-category")]
        public async Task<IActionResult> UpdateCategory(string userId, string name, [FromBody] Category categoryInput)
        {
            await _todoListService.UpdateCategory(userId, name, categoryInput);

            return Ok();
        }

        //[HttpPost]
        //[Route("remove-category/{id}")]
        //public async Task<IActionResult> RemoveCategory(int id)
        //{
        //    var categoryCreation = await _todoListService.RemoveCategory(id);

        //    return Ok(categoryCreation);
        //}

        //#endregion

        //#region TodoTask

        //[HttpPost]
        //[Route("add-task")]
        //public async Task<IActionResult> AddTodoTask([FromBody] TodoTaskInput todoTaskInput)
        //{
        //    var categoryCreation = await _todoListService.AddTodoTask(todoTaskInput);

        //    return Ok(categoryCreation);
        //}

        //[HttpPost]
        //[Route("update-task")]
        //public async Task<IActionResult> UpdateTodoTask([FromBody] TodoTaskInput todoTaskInput)
        //{
        //    var categoryCreation = await _todoListService.UpdateTodoTask(todoTaskInput);

        //    return Ok(categoryCreation);
        //}

        //[HttpPost]
        //[Route("remove-task/{id}")]
        //public async Task<IActionResult> RemoveTodoTask(int id)
        //{
        //    var categoryCreation = await _todoListService.RemoveTodoTask(id);

        //    return Ok(categoryCreation);
        //}

        //#endregion
    }
}
