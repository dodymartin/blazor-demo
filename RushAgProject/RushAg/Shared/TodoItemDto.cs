namespace RushAg.Shared;

public class TodoItemDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public bool IsComplete { get; set; } = false;
    
    public DateTime? CompleteDate { get; set; }
    public DateTime CreateDate { get; set; }
    
    public List<TodoStepDto> Steps { get; set; }
}

public class CreateTodoItemDto
{
    public string Name { get; set; }
}

public class UpdateTodoItemDto
{
    public string Name { get; set; }
    public string Notes { get; set; }
}

public class ToggleTodoItemDto
{
    public bool IsComplete { get; set; }
}
