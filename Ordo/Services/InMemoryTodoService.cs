using Ordo.Models;

namespace Ordo.Services;

public class InMemoryTodoService : ITodoService
{
    private readonly List<Todo> _todos = [];
    private readonly object _syncRoot = new();
    private int _nextId = 1;

    public IReadOnlyList<Todo> GetAll()
    {
        lock (_syncRoot)
        {
            return _todos
                .OrderBy(t => t.IsCompleted)
                .ThenBy(t => t.DueDate.HasValue ? 0 : 1)
                .ThenBy(t => t.DueDate)
                .ThenBy(t => t.CreatedAt)
                .ToList();
        }
    }

    public Todo? GetById(int id)
    {
        lock (_syncRoot)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }
    }

    public Todo Create(string name, DateOnly? dueDate)
    {
        var now = DateTimeOffset.UtcNow;
        var todo = new Todo
        {
            Id = GetNextId(),
            Name = name.Trim(),
            DueDate = dueDate,
            IsCompleted = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        lock (_syncRoot)
        {
            _todos.Add(todo);
        }

        return todo;
    }

    public bool Update(int id, string name, DateOnly? dueDate)
    {
        lock (_syncRoot)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null)
            {
                return false;
            }

            todo.Name = name.Trim();
            todo.DueDate = dueDate;
            todo.UpdatedAt = DateTimeOffset.UtcNow;
            return true;
        }
    }

    public bool ToggleCompleted(int id)
    {
        lock (_syncRoot)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null)
            {
                return false;
            }

            todo.IsCompleted = !todo.IsCompleted;
            todo.UpdatedAt = DateTimeOffset.UtcNow;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_syncRoot)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null)
            {
                return false;
            }

            _todos.Remove(todo);
            return true;
        }
    }

    private int GetNextId()
    {
        lock (_syncRoot)
        {
            return _nextId++;
        }
    }
}
