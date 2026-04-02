using Ordo.Models;

namespace Ordo.Services;

public interface ITodoService
{
    IReadOnlyList<Todo> GetAll();
    Todo? GetById(int id);
    Todo Create(string name, DateOnly? dueDate);
    bool Update(int id, string name, DateOnly? dueDate);
    bool ToggleCompleted(int id);
    bool Delete(int id);
}
