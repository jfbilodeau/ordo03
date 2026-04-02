using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ordo.Models;
using Ordo.Services;

namespace Ordo.Pages.Todos;

public class IndexModel(ITodoService todoService) : PageModel
{
    private readonly ITodoService _todoService = todoService;

    public IReadOnlyList<Todo> Todos { get; private set; } = [];

    public void OnGet()
    {
        LoadTodos();
    }

    public IActionResult OnPostToggleComplete(int id)
    {
        _todoService.ToggleCompleted(id);
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        _todoService.Delete(id);
        return RedirectToPage();
    }

    private void LoadTodos()
    {
        Todos = _todoService.GetAll();
    }
}
