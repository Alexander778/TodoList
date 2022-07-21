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

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInput userInput) 
        {
            var userCreation = await _todoListService.AddUser(userInput);

            return Ok(userCreation);
        }
    }
}
