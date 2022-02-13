using RushAg.Shared;
using System.Net.Http.Json;

namespace RushAg.Client.Services;

public class AppState
{
    public event Action OnChange;
    private readonly HttpClient _httpClient;

    private List<TodoItem> _todoItems = new();
    public List<TodoItem> TodoNotCompleted = new();
    public List<TodoItem> TodoCompleted = new();

    public AppState(HttpClient http)
    {
        _httpClient = http;
    }

    public async Task GetTodos()
    {
        _todoItems = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/TodoItem") ?? new();
        SetLists();
    }

    private void SetLists()
    {
        TodoNotCompleted = _todoItems.Where(x => !x.IsComplete).ToList();
        TodoCompleted = _todoItems.Where(x => x.IsComplete).ToList();
        NotifyStateChanged();
    }

    public async void ToggleComplete(TodoItem todo)
    {
        todo.IsComplete = !todo.IsComplete;
        await UpdateTodo(todo);
    }

    public async Task UpdateTodo(TodoItem todo)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/TodoItem/{todo.Id}", todo);
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    public async Task DeleteTodo(TodoItem todo)
    {
        var response = await _httpClient.DeleteAsync($"api/TodoItem/{todo.Id}");
        await GetTodos();
    }

    public async Task AddTodo(TodoItem todo)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/TodoItem", todo);
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
