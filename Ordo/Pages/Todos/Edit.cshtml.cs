using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ordo.Services;

namespace Ordo.Pages.Todos;

public class EditModel(ITodoService todoService) : PageModel
{
    private readonly ITodoService _todoService = todoService;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public IActionResult OnGet(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo is null)
        {
            return NotFound();
        }

        Id = todo.Id;
        Input = new InputModel
        {
            Name = todo.Name,
            DueDate = todo.DueDate
        };

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        Id = id;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updated = _todoService.Update(id, Input.Name, Input.DueDate);
        if (!updated)
        {
            return NotFound();
        }

        return RedirectToPage("/Todos/Index");
    }

    public class InputModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateOnly? DueDate { get; set; }
    }
}
