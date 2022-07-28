using RushAg.Client.ViewModels;
using RushAg.Shared;
using System.Net.Http.Json;

namespace RushAg.Client.Services;

public class AppState
{
    public event Action OnChange;
    private readonly HttpClient _httpClient;

    private List<TodoItemViewModel> _todoItems = new();
    public List<TodoItemViewModel> TodoNotCompleted = new();
    public List<TodoItemViewModel> TodoCompleted = new();
    public TodoItemViewModel? CurrentTodo = null;
    public bool IsLoading = true;

    public AppState(HttpClient http)
    {
        _httpClient = http;
    }

    public async Task GetTodos()
    {
        _todoItems = await _httpClient.GetFromJsonAsync<List<TodoItemViewModel>>("api/TodoItem") ?? new();
        SetLists();
        IsLoading = false;
    }

    private void SetLists()
    {
        TodoNotCompleted = _todoItems.Where(x => !x.IsComplete).ToList();
        TodoCompleted = _todoItems.Where(x => x.IsComplete).ToList();
        NotifyStateChanged();
    }

    public async void ToggleComplete(TodoItemViewModel todo)
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

    public async Task UpdateTodo(TodoItemViewModel todo)
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

    public async Task DeleteTodo(TodoItemViewModel todo)
    {
        if (todo.TodoItemId == CurrentTodo?.TodoItemId)
            CurrentTodo = null;

        var response = await _httpClient.DeleteAsync($"api/TodoItem/{todo.TodoItemId}");
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    public async Task AddTodo(TodoItemViewModel todo)
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
