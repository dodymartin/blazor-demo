using Microsoft.AspNetCore.Mvc;
using RushAg.Server.Data;
using RushAg.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RushAg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly IDataRepository _repository;

        public TodoItemController(IDataRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TodoItemController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            var returnValue = _repository.GetAll();
            return Ok(returnValue);
        }

        // GET api/<TodoItemController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var returnValue = _repository.GetById(id);
            if (returnValue == null)
                return NotFound();

            return Ok(returnValue);
        }

        // POST api/<TodoItemController>
        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody] TodoItem todoItem)
        {
            if (todoItem == null)
                return BadRequest();

            var newTodoItem = _repository.Add(todoItem);
            if (!_repository.Save())
                throw new Exception("Error saving new TodoItem");

            return Ok(newTodoItem);
        }

        // PUT api/<TodoItemController>/5
        [HttpPut("{id}")]
        public ActionResult<TodoItem> Put(int id, [FromBody] TodoItem todoItem)
        {
            if (todoItem == null)
                return BadRequest();

            var updatedTodo = _repository.Update(todoItem);

            if (!_repository.Save())
                throw new Exception("Error updating TodoItem");

            return Ok(updatedTodo);
        }

        // DELETE api/<TodoItemController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var toDelete = _repository.GetById(id);
            if (toDelete == null)
                return NotFound();

            _repository.Delete(toDelete);
            if (!_repository.Save())
                throw new Exception("Error deleting TodoItem");

            return NoContent();
        }
    }
}
