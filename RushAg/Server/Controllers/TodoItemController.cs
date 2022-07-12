using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RushAg.Infrastructure.Data;
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
        public ActionResult<IEnumerable<TodoItemDto>> Get()
        {
            var returnValue = _repository.GetAll();
            return Ok(returnValue);
        }

        // GET api/<TodoItemController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoItemDto> Get(int id)
        {
            var returnValue = _repository.GetById(id);
            if (returnValue == null)
                return NotFound();

            return Ok(returnValue);
        }

        // POST api/<TodoItemController>
        //[HttpPost]
        //public ActionResult<TodoItemDto> Post([FromBody] TodoItemDto todoItem)
        //{
        //    if (todoItem == null)
        //        return BadRequest();

        //    var newTodoItem = _repository.Add(todoItem);
        //    if (!_repository.Save())
        //        throw new Exception("Error saving new TodoItem");

        //    return Ok(newTodoItem);
        //}

        // PUT api/<TodoItemController>/5
        //[HttpPut("{id}")]
        //public ActionResult<TodoItemDto> Put(int id, [FromBody] object todoItem)
        //{
        //    var result = JsonConvert.DeserializeObject<TodoItemDto>(todoItem.ToString());
        //    if (todoItem == null)
        //        return BadRequest();

        //    //var updatedTodo = _repository.Update(result);

        //    if (!_repository.Save())
        //        throw new Exception("Error updating TodoItem");

        //    return Ok(updatedTodo);
        //}

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
