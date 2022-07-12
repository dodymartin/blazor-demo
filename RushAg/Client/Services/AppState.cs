using RushAg.Client.ViewModels;
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
        todo.IsComplete = !todo.IsComplete;
        await UpdateTodo(todo);
    }

    public async Task UpdateTodo(TodoItemViewModel todo)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/TodoItem/{todo.TodoItemId}", todo);
        response.EnsureSuccessStatusCode();

        CurrentTodo = null;
        await GetTodos();
    }

    public async Task DeleteTodo(TodoItemViewModel todo)
    {
        if (todo.TodoItemId == CurrentTodo?.TodoItemId)
            CurrentTodo = null;

        var response = await _httpClient.DeleteAsync($"api/TodoItem/{todo.TodoItemId}");
        await GetTodos();
    }

    public async Task AddTodo(TodoItemViewModel todo)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/TodoItem", todo);
        response.EnsureSuccessStatusCode();

        await GetTodos();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
