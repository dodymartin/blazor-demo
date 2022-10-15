using RushAg.Shared;
using System.Net.Http.Json;

namespace RushAg.Client.Services;

public class AppState
{
    public event Action OnChange;
    private readonly HttpClient _httpClient;

    private List<TodoItemDto> _todoItems = new();
    public List<TodoItemDto> TodoNotCompleted = new();
    public List<TodoItemDto> TodoCompleted = new();
    public TodoItemDto? CurrentTodo = null;
    public bool IsLoading = true;

    public AppState(HttpClient http)
    {
        _httpClient = http;
    }

    public async Task GetTodos()
    {
        _todoItems = await _httpClient.GetFromJsonAsync<List<TodoItemDto>>("api/TodoItem") ?? new();
        SetLists();
        IsLoading = false;
    }

    private void SetLists()
    {
        TodoNotCompleted = _todoItems.Where(x => !x.IsComplete).ToList();
        TodoCompleted = _todoItems.Where(x => x.IsComplete).ToList();
        NotifyStateChanged();
    }

    public async void ToggleComplete(TodoItemDto todo)
    {
        try
        {
            var request = new ToggleTodoItemDto() { IsComplete = !todo.IsComplete };
            var response = await _httpClient.PutAsJsonAsync($"api/TodoItem/toggle/{todo.TodoItemId}", request);
            response.EnsureSuccessStatusCode();

            await GetTodos();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task UpdateTodo(TodoItemDto todo)
    {
        var request = new UpdateTodoItemDto()
        {
            Name = todo.Name,
            Notes = todo.Notes
        };

        var response = await _httpClient.PutAsJsonAsync($"api/TodoItem/{todo.TodoItemId}", request);
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    public async Task AddStep(CreateTodoStepDto step)
    {
        var request = new TodoStepDto()
        {
            StepName = step.StepName
        };

        var response = await _httpClient.PutAsJsonAsync($"api/TodoItem/step/{step.ParentId}", request);
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    public async Task DeleteTodo(TodoItemDto todo)
    {
        if (todo.TodoItemId == CurrentTodo?.TodoItemId)
            CurrentTodo = null;

        var response = await _httpClient.DeleteAsync($"api/TodoItem/{todo.TodoItemId}");
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    public async Task AddTodo(TodoItemDto todo)
    {
        var request = new CreateTodoItemDto
        {
            Name = todo.Name
        };

        var response = await _httpClient.PostAsJsonAsync($"api/TodoItem", request);
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
