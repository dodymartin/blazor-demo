using Azure.Core;
using BlazorDemo.Core.Entities;
using BlazorDemo.Core.Interfaces;
using BlazorDemo.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.Server.Api
{
    public static class TodoApi
    {
        public static WebApplication AddTodoApis(this WebApplication app)
        {

           var todoGroup = app.MapGroup("/api/todo");

            //api/todo
            todoGroup.MapGet("/", async (IRepository repo) =>
                await repo.GetAllAsync());

            todoGroup.MapGet("/{id}", async (int id, IRepository repo) =>
                await repo.GetByIdAsync(id) 
                    is TodoItem todo 
                    ? Results.Ok(todo) 
                    : Results.NotFound());

            //TODO: Write a query instead of using GetAll
            todoGroup.MapPost("/", async (CreateTodoItemDto todo, IRepository repo) =>
            {
                var existingItem = await repo.GetAllAsync();
                existingItem = existingItem.Where(t => t.Name == todo.Name);

                if (existingItem.Any())
                {
                    //TODO: DuplicateException type creation
                    throw new Exception($"A TodoItem with name {todo.Name} already exists");
                }

                var newTodoItem = new TodoItem(todo.Name);
                newTodoItem = await repo.AddAsync(newTodoItem);

                var dto = new TodoItemDto
                {
                    Id = newTodoItem.Id,
                    Name = newTodoItem.Name
                };

                return Results.Ok(dto);
            });

            todoGroup.MapPut("/{id}", async (int id, [FromBody]UpdateTodoItemDto request, [FromServices]IRepository repo) =>
            {
                if (request == null)
                    return Results.BadRequest();

                var toUpdate = await repo.GetByIdAsync(id);
                if (toUpdate == null)
                    return Results.NotFound();

                toUpdate.Update(request.Name, request.Notes);
                var updatedTodo = await repo.UpdateAsync(toUpdate);
                
                return Results.Ok(updatedTodo);
            });

            todoGroup.MapPut("/toggle/{id}", async (int id, [FromBody] ToggleTodoItemDto request, [FromServices] IRepository repo) =>
            {
                if (request == null)
                    return Results.BadRequest();

                var toUpdate = await repo.GetByIdAsync(id);
                if (toUpdate == null)
                    return Results.NotFound();

                toUpdate.SetIsComplete(request.IsComplete);

                var updatedTodo = await repo.UpdateAsync(toUpdate);

                return Results.Ok(updatedTodo);
            });

            todoGroup.MapPut("/{id}/step", async (int id, [FromBody] CreateTodoStepDto request, [FromServices] IRepository repo) =>
            {
                if (request == null)
                    return Results.BadRequest();

                var parentItem = await repo.GetByIdAsync(id);
                if (parentItem == null)
                    return Results.NotFound();

                var step = new TodoStep(request.Name);
                parentItem.AddStep(step);

                var updatedTodo = await repo.UpdateAsync(parentItem);

                return Results.Ok(updatedTodo);
            });

            todoGroup.MapDelete("/{id}", async (int id, [FromServices] IRepository repo) =>
            {
                var toDelete = await repo.GetByIdAsync(id);
                if (toDelete == null)
                    return Results.NotFound();

                await repo.DeleteAsync(toDelete);

                return Results.NoContent();
            });

            return app;
        }
    }
}
