using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RushAg.Core.Entities;
using RushAg.Infrastructure.Data;
using RushAg.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RushAg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly Repository _repository;

        public TodoItemController(Repository repository)
        {
            _repository = repository;
        }

        // GET: api/<TodoItemController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> Get()
        {
            var returnValue = await _repository.GetAll();
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
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Post([FromBody] CreateTodoItemDto request)
        {
            //TODO: write a query instead of using GetAll
            var existingItem = await _repository.GetAll();
            existingItem = existingItem.Where(t => t.Name == request.Name);

            if (existingItem == null)
            {
                //TODO: DuplicateException type creation
                throw new Exception($"A TodoItem with name {request.Name} already exists");
            }

            var newTodoItem = new TodoItem(request.Name);
            newTodoItem = await _repository.Add(newTodoItem);

            var dto = new TodoItemDto
            {
                TodoItemId = newTodoItem.TodoItemId,
                IsComplete = newTodoItem.IsComplete,
                Name = newTodoItem.Name,
                Notes = newTodoItem.Notes,
                Steps = new List<TodoStepDto>()
            };


            return Ok(dto);
        }

        // PUT api/<TodoItemController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemDto>> Put(int id, [FromBody] UpdateTodoItemDto todoItem)
        {
            try
            {
                if (todoItem == null)
                    return BadRequest();

                var toUpdate = await _repository.GetById(id);
                if (toUpdate == null)
                    return NotFound();

                toUpdate.UpdateStep(todoItem.Name, todoItem.Notes);

                var updatedTodo = await _repository.Update(toUpdate);

                return Ok(updatedTodo);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        //PUT api/<TodoItemController/toggle/5
        [HttpPut("toggle/{id}")]
        public async Task<ActionResult<TodoItemDto>> Put(int id, [FromBody] ToggleTodoItemDto toggleTodoItem)
        {
            try
            {
                if (toggleTodoItem == null)
                    return BadRequest();

                var toUpdate = await _repository.GetById(id);
                if (toUpdate == null)
                    return NotFound();

                toUpdate.SetIsComplete(toggleTodoItem.IsComplete);

                var updatedTodo = await _repository.Update(toUpdate);

                return Ok(updatedTodo);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        //PUT api/step/5
        [HttpPut("step/{id}")]
        public async Task<ActionResult<TodoStepDto>> Put(int id, [FromBody] CreateTodoStepDto createTodoStep)
        {
            if (createTodoStep == null)
                return BadRequest();

            var parentItem = await _repository.GetById(id);
            if (parentItem == null)
                return NotFound();

            var step = new TodoStep(createTodoStep.Name);
            parentItem.AddStep(step);

            var updatedTodo = await _repository.Update(parentItem);

            return Ok(updatedTodo);
        }

        // DELETE api/<TodoItemController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await _repository.GetById(id);
            if (toDelete == null)
                return NotFound();

            await _repository.Delete(toDelete);

            return NoContent();
        }
    }
}
