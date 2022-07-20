using Microsoft.AspNetCore.Mvc;

namespace TodoListAppApi.Controllers
{
    [ApiController]
    [Route("/todolist")]
    public class TodoListController: ControllerBase
    {
        [HttpGet]
        public IActionResult TodoListTest() 
        {
            return Ok("test");
        }
    }
}
