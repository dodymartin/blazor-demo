using BlazorDemoBlazorise.Shared;
using System.Net.Http.Json;

namespace BlazorDemoBlazorise.Client.Services
{
    public class TodoService
    {
        public event Action OnChange;
        private readonly HttpClient _httpClient;

        private List<TodoItemDto> _todoItems = new();
        public List<TodoItemDto> TodoNotCompleted = new();
        public List<TodoItemDto> TodoCompleted = new();
        public TodoItemDto? CurrentTodo = null;
        public bool IsLoading = true;

        public TodoService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task GetTodos()
        {
            try
            {
                _todoItems = await _httpClient.GetFromJsonAsync<List<TodoItemDto>>("api/todo") ?? new();
                SetLists();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
                var response = await _httpClient.PutAsJsonAsync($"api/todo/toggle/{todo.Id}", request);
                response.EnsureSuccessStatusCode();

                await GetTodos();
            }
            catch (Exception ex)
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

            var response = await _httpClient.PutAsJsonAsync($"api/todo/{todo.Id}", request);
            response.EnsureSuccessStatusCode();

            await GetTodos();
        }

        public async Task AddStep(CreateTodoStepDto step)
        {
            var request = new TodoStepDto()
            {
                Name = step.Name
            };

            var response = await _httpClient.PutAsJsonAsync($"api/todo/step/{step.TodoItemId}", request);
            response.EnsureSuccessStatusCode();

            await GetTodos();
        }

        public async Task DeleteTodo(TodoItemDto todo)
        {
            if (todo.Id == CurrentTodo?.Id)
                CurrentTodo = null;

            var response = await _httpClient.DeleteAsync($"api/todo/{todo.Id}");
            response.EnsureSuccessStatusCode();

            await GetTodos();
        }

        public async Task AddTodo(TodoItemDto todo)
        {
            var request = new CreateTodoItemDto
            {
                Name = todo.Name
            };

            var response = await _httpClient.PostAsJsonAsync($"api/todo", request);
            response.EnsureSuccessStatusCode();

            await GetTodos();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
